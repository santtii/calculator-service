using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Core.Models
{
    public class OperationsRequestModel
    {
        [Required]
        public string? Id { get; set; }
    }
}
