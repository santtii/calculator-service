using CalculatorAPI.Core.Models;

namespace CalculatorAPI.Core.Interfaces.Services
{
    public interface IClientService
    {
        void AddTrackingId(string? trackingId);
        Task<AdditionResponseModel?> AddRequestAsync(ICollection<decimal> addends);
        Task<SubtractionResponseModel?> SubtractRequestAsync(decimal minuend, decimal subtrahend);
        Task<MultiplyResponseModel?> MultiplyRequestAsync(ICollection<decimal> factors);
        Task<DivisionResponseModel?> DivisionRequestAsync(decimal dividend, decimal divisor);
        Task<SquareRootResponseModel?> SquareRootRequestAsync(decimal number);
        Task<OperationsResponseModel?> JournalQuery(string? trackingId);
    }
}
