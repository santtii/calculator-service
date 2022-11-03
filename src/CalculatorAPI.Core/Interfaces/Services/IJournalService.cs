using CalculatorAPI.Core.Models;
using CalculatorAPI.Core.Services;

namespace CalculatorAPI.Core.Interfaces.Services
{
    public interface IJournalService
    {
        OperationModel? AddOperation(string trackingId, MathOperationType operation, decimal op1, decimal op2, string result, DateTime? date = null);
        OperationModel? AddOperation(string trackingId, MathOperationType operation, ICollection<decimal> operands, string result, DateTime? date = null);
        OperationModel? AddUnaryOperation(string trackingId, MathOperationType operation, decimal operand, string result, DateTime? date = null);
        OperationsResponseModel Operations(string trackingId);
    }
}
