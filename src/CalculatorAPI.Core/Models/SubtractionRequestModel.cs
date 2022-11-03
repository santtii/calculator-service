using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Core.Models
{
    public class SubtractionRequestModel
    {
        [Required]
        public decimal Minuend { get; set; }
        [Required]
        public decimal Subtrahend { get; set; }
    }
}
