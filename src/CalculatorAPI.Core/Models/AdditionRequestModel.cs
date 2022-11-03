using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Core.Models
{
    public class AdditionRequestModel
    {
        [Required, MinLength(2)]
        public ICollection<decimal>? Addends { get; set; }
    }
}
