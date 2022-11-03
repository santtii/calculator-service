using CalculatorAPI.Core.Interfaces.Services;
using CalculatorAPI.Core.Models;
using Microsoft.Extensions.Logging;
using Ninject.Infrastructure;

namespace CalculatorAPI.Core.Services
{
    public enum MathOperationType { None, Sum, Sub, Mul, Div, Sqrt }

    public class JournalService : IJournalService
    {
        private readonly ILogger<CalculatorService> _logger;

        private Multimap<string, OperationModel> _operations { get; set; } = new Multimap<string, OperationModel>();

        private class MathOperation
        {
            public string? Operator { get; }

            public MathOperation(MathOperationType mathOperationType)
            {
                switch (mathOperationType)
                {
                    case MathOperationType.Sum:
                        Operator = "+";
                        break;
                    case MathOperationType.Sub:
                        Operator = "-";
                        break;
                    case MathOperationType.Mul:
                        Operator = "*";
                        break;
                    case MathOperationType.Div:
                        Operator = "/";
                        break;
                    case MathOperationType.Sqrt:
                        Operator = "√";
                        break;
                    default:
                        break;
                }
            }
        }

        public JournalService(ILogger<CalculatorService> logger)
        {
            _logger = logger;
        }

        public OperationModel? AddOperation(
            string trackingId, MathOperationType operation, decimal op1, decimal op2, string result, DateTime? date = null)
        {
            return AddOperation(trackingId, operation, new List<decimal> { op1, op2 }, result, date);
        }

        public OperationModel? AddOperation(
            string trackingId, MathOperationType operation, ICollection<decimal> operands, string result, DateTime? date = null)
        {
            if ((operation == MathOperationType.None) || (operation == MathOperationType.Sqrt))
            {
                return null;
            }

            var mathOperation = new MathOperation(operation);

            var operationModel = new OperationModel
            {
                Operation = operation.ToString(),
                Calculation = string.Join($" {mathOperation.Operator} ", operands.ToArray()) + $" = {result}",
                Date = date ?? DateTime.UtcNow
            };
            _operations.Add(trackingId, operationModel);
            _logger.LogInformation("Operation: [Id({Id}), {OperationModel}]", trackingId, operationModel);
            return operationModel;
        }

        public OperationModel? AddUnaryOperation(
            string trackingId, MathOperationType operation, decimal operand, string result, DateTime? date = null)
        {
            if ((operation == MathOperationType.None) || (operation == MathOperationType.Mul) || (operation == MathOperationType.Div))
            {
                return null;
            }

            var mathOperation = new MathOperation(operation);

            var operationModel = new OperationModel
            {
                Operation = operation.ToString(),
                Calculation = $"{mathOperation.Operator}{operand} = {result}",
                Date = date ?? DateTime.UtcNow
            };
            _operations.Add(trackingId, operationModel);
            _logger.LogInformation("Operation: [Id({Id}), {OperationModel}]", trackingId, operationModel);
            return operationModel;
        }

        public OperationsResponseModel Operations(string trackingId)
        {
            return new OperationsResponseModel
            {
                Operations = _operations.Where(x => x.Key == trackingId)?.Select(y => y.Value)?.FirstOrDefault()
            };
        }
    }
}
