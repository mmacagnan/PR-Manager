using System.ComponentModel.DataAnnotations;
using PrManager.BL.Models.Commons;

namespace PrManager.BL.Models
{
    public class Role : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}