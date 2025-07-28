using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PerformanceManagementChart.Server.Controllers;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;
using PerformanceManagementChart.Server.Services.ApiServices.ActivityApiServiceFactory;

namespace PerformanceManagementChart.Tests.Controllers;

public class FitnessChartControllerTests
{
    private readonly FitnessChartController _controller;
    private readonly string _serviceType = "intervals";
    private readonly Mock<IActivityApiServiceFactory> _mockActivityApiServiceFactory;
    private readonly Mock<IMetricsService> _mockMetricsService;

    public FitnessChartControllerTests()
    {
        NullLogger<FitnessChartController> logger = NullLogger<FitnessChartController>.Instance;

        _mockActivityApiServiceFactory = new Mock<IActivityApiServiceFactory>();
        _mockMetricsService = new Mock<IMetricsService>();

        _controller = new FitnessChartController(
            logger,
            _mockActivityApiServiceFactory.Object,
            _mockMetricsService.Object
        );
    }

    [Fact]
    public void FitnessChartController_Exists()
    {
        // Arrange

        // Act

        // Assert
        Assert.NotNull(_controller);
    }

    [Fact]
    public async Task FitnessChartController_ReturnsActionResult()
    {
        // Arrange
        // Act
        var result = await _controller.GetFitnessChartData(
            "athleteId",
            DateTime.Now.AddDays(-30),
            DateTime.Now
        );
        // Assert
        Assert.IsType<ActionResult<IEnumerable<ActivityDto>>>(result);
    }

    [Fact]
    public async Task GetFitnessChartData_CallsServiceMethods()
    {
        // Arrange
        var mockApiService = new Mock<IActivityApiService>();

        mockApiService
            .Setup(x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()
                )
            )
            .ReturnsAsync(new List<ActivityDto> { new ActivityDto() });

        _mockActivityApiServiceFactory
            .Setup(x => x.GetActivityApiService(_serviceType))
            .Returns(mockApiService.Object);

        _mockMetricsService
            .Setup(x => x.TransformApiData(It.IsAny<List<ActivityDto>>(), It.IsAny<DateTime>()))
            .Returns(new List<ActivityDto> { new ActivityDto() });


        // Act
        var result = await _controller.GetFitnessChartData(
            "athleteId",
            DateTime.Now.AddDays(-30),
            DateTime.Now
        );

        // Assert
        mockApiService.Verify(
            x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()
                ),
            Times.Once
        );
        _mockMetricsService.Verify(
            x => x.TransformApiData(It.IsAny<List<ActivityDto>>(), It.IsAny<DateTime>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetFitnessChartData_NoActivities_ReturnsNotFound()
    {
        // Arrange
        var mockApiService = new Mock<IActivityApiService>();
        mockApiService
            .Setup(x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()
                )
            )
            .ReturnsAsync(new List<ActivityDto>());

        _mockActivityApiServiceFactory
            .Setup(x => x.GetActivityApiService(_serviceType))
            .Returns(mockApiService.Object);

        // Act
        var result = await _controller.GetFitnessChartData(
            "athleteId",
            DateTime.Now.AddDays(-30),
            DateTime.Now
        );

        // Assert
        mockApiService.Verify(
            x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()
                ),
            Times.Once
        );

        Assert.IsType<NotFoundObjectResult>(result.Result);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.NotNull(notFoundResult);
        Assert.Equal("No activities found for the specified date range.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetFitnessChartData_InvalidParameterString_ReturnsBadRequest()
    {
        // Arrange
        string athleteId = null;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(-1);

        // Act
        var result = await _controller.GetFitnessChartData(athleteId, startDate, endDate);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal("Invalid parameters provided.", badRequestResult.Value);
    }

    // Verify that end date before start date returns BadRequest
    [Fact]
    public async Task GetFitnessChartData_EndDateBeforeStartDate_ReturnsBadRequest()
    {
        // Arrange
        string athleteId = "athleteId";
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(-1);

        // Act
        var result = await _controller.GetFitnessChartData(athleteId, startDate, endDate);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal("Invalid parameters provided.", badRequestResult.Value);
    }
}
