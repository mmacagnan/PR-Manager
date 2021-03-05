using System.ComponentModel.DataAnnotations;

namespace PrManager.UI.Models
{
    public class CongregationDetailViewModel
    {
        [Required(ErrorMessage = "Please tell us the congregation name")]
        public string CongregationName { get; set; }
        
        [Required(ErrorMessage = "Please, tell the congregation number")]
        public string CongregationNumber { get; set; }
    }
}