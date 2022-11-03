using CalculatorAPI.Core.Interfaces.Services;
using CalculatorAPI.Core.Models;

namespace CalculatorAPI.Core.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IJournalService _journalService;

        public CalculatorService(IJournalService journalService)
        {
            _journalService = journalService;
        }

        public AdditionResponseModel Add(ICollection<decimal> addends, string? trackingId = null)
        {
            var result = new AdditionResponseModel { Sum = 0m };

            foreach (var addend in addends)
            {
                result.Sum += addend;
            }
            if (trackingId != null)
            {
                _journalService.AddOperation(trackingId, MathOperationType.Sum, addends, $"{result.Sum}");
            }
            return result;
        }

        public SubtractionResponseModel Sub(decimal minuend, decimal subtrahend, string? trackingId = null)
        {
            var result = new SubtractionResponseModel { Difference = minuend - subtrahend };

            if (trackingId != null)
            {
                _journalService.AddOperation(trackingId, MathOperationType.Sub, minuend, subtrahend, $"{result.Difference}");
            }
            return result;

        }

        public MultiplyResponseModel Mult(ICollection<decimal> factors, string? trackingId = null)
        {
            var result = new MultiplyResponseModel { Product = 1m };

            foreach (var factor in factors)
            {
                result.Product *= factor;
            }
            if (trackingId != null)
            {
                _journalService.AddOperation(trackingId, MathOperationType.Mul, factors, $"{result.Product}");
            }
            return result;
        }

        public DivisionResponseModel Div(decimal dividend, decimal divisor, string? trackingId = null)
        {
            var result = new DivisionResponseModel { Quotient = (int)(dividend / divisor), Remainder = dividend % divisor };

            if (trackingId != null)
            {
                _journalService.AddOperation(trackingId, MathOperationType.Div, dividend, divisor, $"({result.Quotient}, {result.Remainder})");
            }
            return result;
        }

        public SquareRootResponseModel Sqrt(decimal number, string? trackingId = null)
        {
            var result = new SquareRootResponseModel { Square = (decimal)Math.Sqrt((double)number) };

            if (trackingId != null)
            {
                _journalService.AddUnaryOperation(trackingId, MathOperationType.Sqrt, number, $"{result.Square}");
            }
            return result;
        }
    }
}
