using System.ComponentModel.DataAnnotations.Schema;
using PrManager.BL.Models.Commons;

namespace PrManager.BL.Models
{
    public class Congregation : BaseEntity
    {
        public string CongregationName { get; set; }
        public string CongregationNumber { get; set; }
        
        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
    }
}