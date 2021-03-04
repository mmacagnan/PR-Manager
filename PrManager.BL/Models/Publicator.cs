using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrManager.BL.Models.Commons;

namespace PrManager.BL.Models
{
    public class Publicator : BaseEntity
    {
        [Required]
        [MaxLength(80)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(191)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [ForeignKey("Congregation")]
        public int CongregationId { get; set; }
        public virtual Congregation Congregation { get; set; }
    }
}