using System.ComponentModel.DataAnnotations;

namespace CoreLayer.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
