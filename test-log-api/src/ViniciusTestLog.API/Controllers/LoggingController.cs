using Microsoft.AspNetCore.Mvc;
using ViniciusTestLog.API.Models;
using ViniciusTestLog.API.Services;

namespace ViniciusTestLog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogPersistorService _logPersistorService;
        private readonly ILogQueryService _logQueryService;
        public LoggingController(ILogPersistorService logPersistorService, ILogQueryService logQueryService)
        {
            _logPersistorService = logPersistorService;
            _logQueryService = logQueryService;
        }

        [HttpPost]
        [Route("persistLogs")]
        public async Task<IActionResult> PersistLogs()
        {
            await _logPersistorService.PersistLogFile();
            return Ok();
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<string[]>> GetCategories()
        {
            return Ok(await _logQueryService.GetCategories());
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetFilteredLogs([FromBody] LogDataFilter filter)
        {
            return Ok(await _logQueryService.GetFiltered(filter));
        }

    }
}