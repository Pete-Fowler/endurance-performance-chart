using Microsoft.AspNetCore.Mvc;

namespace PerformanceManagementChart.Server.Controllers
{
    [ApiController]
    public class FitnessChartController : ControllerBase
    {
        private readonly ILogger _logger;

        public FitnessChartController(ILogger logger)
        {
            _logger = logger;
        }

        // [HttpGet(Name = "GetPerformanceData")]
        // public IEnumerable<PerformanceData> Get()
        // {
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //         TemperatureC = Random.Shared.Next(-20, 55),
        //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }
    }
}
