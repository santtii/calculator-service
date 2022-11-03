using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Core.Models
{
    public class DivisionRequestModel
    {
        [Required]
        public decimal Dividend { get; set; }
        [Required, RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "The field {0} must be diferent to zero")]
        public decimal Divisor { get; set; }
    }
}
