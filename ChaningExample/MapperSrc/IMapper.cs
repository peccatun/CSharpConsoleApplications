using ChaningExample.Entities;
using System;
using System.Linq.Expressions;

namespace ChaningExample.MapperSrc
{
    public interface IMapper<T> where T: BaseEntity
    {
        void Insert(T model);

        IMapper<T> Update(T model);

        public IMapper<T> WhereMap(Expression<Func<T, bool>> expression);

        public string ExecuteNonQuery();
    }


}
