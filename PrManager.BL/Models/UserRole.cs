using System.ComponentModel.DataAnnotations.Schema;
using PrManager.BL.Models.Commons;

namespace PrManager.BL.Models
{
    public class UserRole : BaseEntity
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}