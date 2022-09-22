using ChaningExample.Attributes;

namespace ChaningExample.Entities
{
    public class BaseEntity
    {
        [MapFrom("Id")]
        public int Id { get; set; }

        [MapFrom("IsDel")]
        public short IsDel { get; set; }
    }
}
