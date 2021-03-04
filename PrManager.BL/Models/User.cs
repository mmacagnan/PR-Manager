using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrManager.BL.Models.Commons;

namespace PrManager.BL.Models
{
    public class User : BaseEntity
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
        
        [Required]
        [MaxLength(16)]
        public string UserName { get; set; }
        
        [Required]
        public string HashedPassword { get; set; }

        [ForeignKey("Publicator")]
        public int? PublicatorId { get; set; }
        public virtual Publicator Publicator { get; set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}