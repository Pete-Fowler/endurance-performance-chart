using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;

namespace PerformanceManagementChart.Server.Controllers
{
    [ApiController]
    [Route("api/fitness-chart")]
    public class FitnessChartController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IActivityApiService _activityApiService;
        private readonly IMetricsService _metricsService;

        public FitnessChartController(
            ILogger logger,
            IActivityApiService activityApiService,
            IMetricsService metricsService
        )
        {
            _logger = logger;
            _activityApiService = activityApiService;
            _metricsService = metricsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetFitnessChartData(
            string athleteId,
            DateOnly startDate,
            DateOnly endDate
        )
        {
            try
            {
                if (string.IsNullOrEmpty(athleteId) || startDate > endDate)
                {
                    return BadRequest("Invalid parameters provided.");
                }

                _logger.LogInformation(
                    "Retrieving fitness chart data for athlete {AthleteId} from {StartDate} to {EndDate}",
                    athleteId,
                    startDate,
                    endDate
                );

                List<ActivityDto> rawActivityData = await _activityApiService.LoadActivitiesAsync(
                    athleteId,
                    startDate,
                    endDate
                );
                if (rawActivityData == null || rawActivityData.Count == 0)
                {
                    _logger.LogWarning(
                        "No activities found for athlete {AthleteId} from {StartDate} to {EndDate}",
                        athleteId,
                        startDate,
                        endDate
                    );
                    return NotFound("No activities found for the specified date range.");
                }

                List<ActivityDto> activityData = _metricsService.TransformApiData(rawActivityData);

                _logger.LogInformation(
                    "Retrieved {ActivityCount} activities for athlete {AthleteId}",
                    activityData.Count,
                    athleteId
                );

                return Ok(activityData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fitness chart data");
                return Problem("Internal server error");
            }
        }
    }
}
