using CalculatorAPI.Core.Interfaces.Services;
using CalculatorAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId, [FromBody] AdditionRequestModel model)
        {
            return Ok(_calculatorService.Add(model.Addends, trackingId));
        }

        [HttpPost("Sub")]
        public IActionResult Sub([FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId, [FromBody] SubtractionRequestModel model)
        {
            return Ok(_calculatorService.Sub(model.Minuend, model.Subtrahend, trackingId));
        }

        [HttpPost("Mult")]
        public IActionResult Mult([FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId, [FromBody] MultiplyRequestModel model)
        {
            return Ok(_calculatorService.Mult(model.Factors, trackingId));
        }

        [HttpPost("Div")]
        public IActionResult Div([FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId, [FromBody] DivisionRequestModel model)
        {
            return Ok(_calculatorService.Div(model.Dividend, model.Divisor, trackingId));
        }

        [HttpPost("Sqrt")]
        public IActionResult Sqrt([FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId, [FromBody] SquareRootRequestModel model)
        {
            return Ok(_calculatorService.Sqrt(model.Number, trackingId));
        }
    }
}
