using System.ComponentModel.DataAnnotations;

namespace CoreLayer.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
