using CalculatorAPI.Core.Interfaces.Services;
using CalculatorAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("Query")]
        public IActionResult Query([FromBody] OperationsRequestModel model)
        {
            return Ok(_journalService.Operations(model.Id));
        }
    }
}
