

using ChaningExample.Attributes;
using ChaningExample.Dtos;
using ChaningExample.Entities;
using ChaningExample.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ChaningExample.MapperSrc
{
    public sealed class Mapper<T> : IMapper<T> where T : BaseEntity
    {
        private StringBuilder updateStatementSb;
        private readonly StringBuilder whereStatementSb;

        public Mapper()
        {
            updateStatementSb = new StringBuilder();
            whereStatementSb = new StringBuilder();
        }

        public void Insert(T model)
        {
            throw new NotImplementedException();
        }

        public IMapper<T> Update(T model)
        {
            GenerateUpdateStatement(model);

            return this;
        }

        private string GenerateInsertStatement(T model)
        {
            throw new NotImplementedException();
        }

        private void GenerateUpdateStatement(T model)
        {
            string tableName = GetTableName();

            IEnumerable<ColumnPair> pairs = GenColumns(model);

            updateStatementSb.Append("UPDATE ");
            updateStatementSb.Append(tableName);
            updateStatementSb.Append(" SET ");
            updateStatementSb.Append(string.Join(", ", pairs));
        }

        private IEnumerable<ColumnPair> GenColumns(T model)
        {
            List<ColumnPair> pairs = new List<ColumnPair>();

            PropertyInfo[] propInfos = model.GetType().GetProperties();

            foreach (var prop in propInfos)
            {
                //MapFromAttribute attr = prop.GetCustomAttribute<MapFromAttribute>();
                if (!(prop.GetCustomAttribute(typeof(MapFromAttribute)) is MapFromAttribute attr))
                {
                    continue;
                }

                string columnName = attr.Name;

                string columnValue;
                object value = prop.GetValue(model);

                if (value is int id)
                {
                    if (id == default)
                    {
                        continue;
                    }

                }

                if (prop.PropertyType == typeof(DateTime))
                {
                    columnValue = $"CAST('{(value):MM.dd.yyyy}' as DATETIME)";
                }
                else
                {
                    columnValue = value.ToString();
                }

                if (prop.PropertyType.Equals(typeof(string)))
                {
                    columnValue = $"'{columnValue}'";
                }

                ColumnPair pair = new ColumnPair
                {
                    ColumnName = columnName,
                    Value = columnValue,
                };

                pairs.Add(pair);

            }

            return pairs;
        }

        private string GetTableName()
        {
            return ((MapFromAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(MapFromAttribute))).Name;
        }

        public IMapper<T> WhereMap(Expression<Func<T, bool>> expression)
        {
            ParseExpression(expression);
            //PropertyInfo property = typeof(T).GetProperty(member);


            return this;
        }

        private void ParseExpression(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Lambda)
            {
                var lambdaExpression = (LambdaExpression)expr;

                var type = lambdaExpression.Body.GetType().ToString();
                ParseExpression(lambdaExpression.Body);
            }

            if (expr.NodeType == ExpressionType.Equal)
            {
                BinaryExpression expression = (BinaryExpression)expr;

                ParseExpression(expression.Left);
                whereStatementSb.Append(" = ");
                ParseExpression(expression.Right);
            }

            if (expr.NodeType == ExpressionType.GreaterThan)
            {
                BinaryExpression expression = (BinaryExpression)expr;
                ParseExpression(expression.Left);
                whereStatementSb.Append(" > ");
                ParseExpression(expression.Right);
            }

            if (expr.NodeType == ExpressionType.LessThan)
            {
                BinaryExpression expression = (BinaryExpression)expr;

                ParseExpression(expression.Left);
                whereStatementSb.Append(" < ");
                ParseExpression(expression.Right);
            }

            if (expr.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                BinaryExpression expression = (BinaryExpression)expr;
                ParseExpression(expression.Left);

                whereStatementSb.Append(" >= ");
                ParseExpression(expression.Right);
            }

            if (expr.NodeType == ExpressionType.LessThanOrEqual)
            {
                BinaryExpression expression = (BinaryExpression)expr;
                ParseExpression(expression.Left);

                whereStatementSb.Append(" <= ");
                ParseExpression(expression.Right);
            }

            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression expression = (MemberExpression)expr;
                string memberName = expression.Member.Name;

                string tableName = GetTableNameByPropName(memberName);

                if (whereStatementSb.Length == 0)
                {
                    whereStatementSb.Append(" where ");
                }

                whereStatementSb.Append(tableName);
            }

            if (expr.NodeType == ExpressionType.Constant)
            {
                ConstantExpression expression = (ConstantExpression)expr;
                object value = expression.Value;
                string valueString = string.Empty;
                if (value is decimal dec)
                {
                    valueString = dec.ToString();
                }

                if (value is double dou)
                {
                    valueString = dou.ToString();
                }

                if (value is int num)
                {
                    valueString = num.ToString();
                }

                if (value is string str)
                {
                    valueString = $"'{str}'";
                }

                whereStatementSb.Append(valueString);
            }

            if (expr.NodeType == ExpressionType.Convert)
            {
                UnaryExpression expression = (UnaryExpression)expr;
                MemberExpression memberExpression = (MemberExpression)expression.Operand;
                string memberName = memberExpression.Member.Name;
                string tableName = GetTableNameByPropName(memberName);

                if (whereStatementSb.Length == 0)
                {
                    whereStatementSb.Append(" where ");
                }

                whereStatementSb.Append(tableName);
            }

            if (expr.NodeType == ExpressionType.AndAlso)
            {
                BinaryExpression expression = (BinaryExpression)expr;

                ParseExpression(expression.Left);

                whereStatementSb.Append(" and ");

                ParseExpression(expression.Right);
            }

            if (expr.NodeType == ExpressionType.OrElse)
            {
                BinaryExpression expression = (BinaryExpression)expr;

                ParseExpression(expression.Left);
                whereStatementSb.Append(" or ");
                ParseExpression(expression.Right);
            }
        }

        private string GetTableNameByPropName(string propName)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(propName);

            if (!(propInfo.GetCustomAttribute(typeof(MapFromAttribute)) is MapFromAttribute atr))
            {
                throw new MapFromNotFoundException();
            }

            return atr.Name;
        }

        public string ExecuteNonQuery()
        {
            string statement = updateStatementSb.Append(whereStatementSb).ToString();
            updateStatementSb.Clear();
            whereStatementSb.Clear();


            return statement;
        }
    }
}
