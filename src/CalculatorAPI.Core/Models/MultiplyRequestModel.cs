using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Core.Models
{
    public class MultiplyRequestModel
    {
        [Required, MinLength(2)]
        public ICollection<decimal>? Factors { get; set; }
    }
}
