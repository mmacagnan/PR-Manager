using System.ComponentModel.DataAnnotations;

namespace PrManager.BL.Models.Commons
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}