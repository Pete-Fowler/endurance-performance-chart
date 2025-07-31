using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;
using PerformanceManagementChart.Server.Services.ApiServices.ActivityApiServiceFactory;

namespace PerformanceManagementChart.Server.Controllers
{
    [ApiController]
    [Route("api/fitness-chart")]
    public class FitnessChartController : ControllerBase
    {
        private readonly ILogger<FitnessChartController> _logger;
        private readonly IActivityApiServiceFactory _activityApiServiceFactory;
        private readonly IMetricsService _metricsService;

        public FitnessChartController(
            ILogger<FitnessChartController> logger,
            IActivityApiServiceFactory apiServiceFactory,
            IMetricsService metricsService
        )
        {
            _logger = logger;
            _activityApiServiceFactory = apiServiceFactory;
            _metricsService = metricsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetFitnessChartData(
            string? athleteId = "i360301",
            DateTime? startDate = null,
            DateTime? endDate = null
        )
        {
            // Using default values for simple demo app. Would be gotten from front
            // end duration selectors
            startDate ??= DateTime.UtcNow.AddMonths(-6);
            endDate ??= DateTime.UtcNow.Date.AddDays(1).AddTicks(-1);

            try
            {
                if (string.IsNullOrEmpty(athleteId) || startDate > endDate)
                {
                    return BadRequest("Invalid parameters provided.");
                }

                // Hard coding the type of service for this demo. For a fully developed app,
                // this would come from user identity / claims info from JWT or session cookie.

                IActivityApiService activityApiService =
                    _activityApiServiceFactory.GetActivityApiService("intervals");

                _logger.LogInformation(
                    "Retrieving fitness chart data for athlete {AthleteId} from {StartDate} to {EndDate}",
                    athleteId,
                    startDate,
                    endDate
                );

                List<ActivityDto> rawActivityData = await activityApiService.LoadActivitiesAsync(
                    athleteId,
                    (DateTime)startDate,
                    (DateTime)endDate
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

                List<ActivityDto> activityData = _metricsService.TransformApiData(
                    rawActivityData,
                    endDate.Value
                );

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
                return Problem(ex.Message);
            }
        }
    }
}
