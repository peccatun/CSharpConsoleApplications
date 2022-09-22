namespace ChaningExample.Dtos
{
    public class ColumnPair
    {
        public string ColumnName { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{ColumnName} = {Value}";
        }
    }
}
