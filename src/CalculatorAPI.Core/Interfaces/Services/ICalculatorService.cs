using CalculatorAPI.Core.Models;

namespace CalculatorAPI.Core.Interfaces.Services
{
    public interface ICalculatorService
    {
        AdditionResponseModel Add(ICollection<decimal> addends, string? trackingId = null);
        SubtractionResponseModel Sub(decimal minuend, decimal subtrahend, string? trackingId = null);
        MultiplyResponseModel Mult(ICollection<decimal> factors, string? trackingId = null);
        DivisionResponseModel Div(decimal dividend, decimal divisor, string? trackingId = null);
        SquareRootResponseModel Sqrt(decimal number, string? trackingId = null);
    }
}
