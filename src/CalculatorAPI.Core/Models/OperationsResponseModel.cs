namespace CalculatorAPI.Core.Models
{
    public class OperationModel
    {
        public string? Operation { get; set; }
        public string? Calculation { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Operation: {Operation}, Calculation: {Calculation}, Date: {Date}";
        }
    }

    public class OperationsResponseModel
    {
        public ICollection<OperationModel>? Operations { get; set; }
    }
}
