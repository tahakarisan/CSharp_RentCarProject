using System.ComponentModel.DataAnnotations;
using CoreLayer.Entities.Abstract;

namespace CoreLayer.Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
