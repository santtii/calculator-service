using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Core.Models
{
    public class SquareRootRequestModel
    {
        [Required, Range(0, double.MaxValue)]
        public decimal Number { get; set; }
    }
}
