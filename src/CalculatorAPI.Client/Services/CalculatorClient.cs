using CalculatorAPI.Core.Interfaces.Services;
using CalculatorAPI.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace CalculatorAPI.Client.Services
{
    public class CalculatorClient : IClientService
    {
        private readonly IConfiguration _configuration;

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public CalculatorClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(_configuration.GetValue<string>("CalculatorAPI:BaseAddress")),
                Timeout = TimeSpan.FromSeconds(_configuration.GetValue<double>("CalculatorAPI:RequestTimeout"))
            };
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public void AddTrackingId(string? trackingId)
        {
            if (trackingId != null)
            {
                _httpClient.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", trackingId);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove("X-Evi-Tracking-Id");
            }
        }

        public async Task<AdditionResponseModel?> AddRequestAsync(ICollection<decimal> addends)
        {
            AdditionResponseModel? result = null;

            var model = new AdditionRequestModel { Addends = addends };

            var requestBody = JsonSerializer.Serialize<AdditionRequestModel?>(model);
            HttpContent httpContent = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("calculator/add", httpContent);
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<AdditionResponseModel?>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            }
            return result;
        }

        public async Task<SubtractionResponseModel?> SubtractRequestAsync(decimal minuend, decimal subtrahend)
        {
            SubtractionResponseModel? result = null;

            var model = new SubtractionRequestModel { Minuend = minuend, Subtrahend = subtrahend };

            var requestBody = JsonSerializer.Serialize<SubtractionRequestModel?>(model);
            HttpContent httpContent = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("calculator/sub", httpContent);
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<SubtractionResponseModel?>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            }
            return result;
        }

        public async Task<MultiplyResponseModel?> MultiplyRequestAsync(ICollection<decimal> factors)
        {
            MultiplyResponseModel? result = null;

            var model = new MultiplyRequestModel { Factors = factors };

            var requestBody = JsonSerializer.Serialize<MultiplyRequestModel?>(model);
            HttpContent httpContent = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("calculator/mult", httpContent);
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<MultiplyResponseModel?>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            }
            return result;
        }

        public async Task<DivisionResponseModel?> DivisionRequestAsync(decimal dividend, decimal divisor)
        {
            DivisionResponseModel? result = null;

            var model = new DivisionRequestModel { Dividend = dividend, Divisor = divisor };

            var requestBody = JsonSerializer.Serialize<DivisionRequestModel?>(model);
            HttpContent httpContent = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("calculator/div", httpContent);
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<DivisionResponseModel?>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            }
            return result;
        }

        public async Task<SquareRootResponseModel?> SquareRootRequestAsync(decimal number)
        {
            SquareRootResponseModel? result = null;

            var model = new SquareRootRequestModel { Number = number };

            var requestBody = JsonSerializer.Serialize<SquareRootRequestModel?>(model);
            HttpContent httpContent = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("calculator/sqrt", httpContent);
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<SquareRootResponseModel?>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            }
            return result;
        }

        public async Task<OperationsResponseModel?> JournalQuery(string? trackingId)
        {
            OperationsResponseModel? result = null;

            var model = new OperationsRequestModel { Id = trackingId };

            var requestBody = JsonSerializer.Serialize<OperationsRequestModel?>(model);
            HttpContent httpContent = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("journal/query", httpContent);
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<OperationsResponseModel?>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            }
            return result;
        }
    }
}
