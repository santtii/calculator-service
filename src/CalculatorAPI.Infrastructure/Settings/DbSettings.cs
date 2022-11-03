using CalculatorAPI.Core.Interfaces.Settings;

namespace CalculatorAPI.Infrastructure.Settings
{
    public class DbSettings : IDbSettings
    {
        public string? DefaultConnection { get; set; }
    }
}
