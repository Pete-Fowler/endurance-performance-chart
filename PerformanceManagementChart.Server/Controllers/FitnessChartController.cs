using Microsoft.AspNetCore.Mvc;
using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Controllers
{
    [ApiController]
    [Route("api/fitness-chart")]
    public class FitnessChartController : ControllerBase
    {
        private readonly ILogger _logger;

        public FitnessChartController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ActivityDto>> GetFitnessChartData()
        {
            return new List<ActivityDto>();
        }
    }
}
