using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PerformanceManagementChart.Server.Controllers;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;

namespace PerformanceManagementChart.Tests.Controllers;

public class FitnessChartControllerTests
{
    private readonly FitnessChartController _controller;
    private readonly Mock<IActivityApiService> _mockApiService;
    private readonly Mock<IMetricsService> _mockMetricsService;

    public FitnessChartControllerTests()
    {
        NullLogger logger = NullLogger.Instance;

        _mockApiService = new Mock<IActivityApiService>();
        _mockMetricsService = new Mock<IMetricsService>();
        _controller = new FitnessChartController(
            logger,
            _mockApiService.Object,
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
            DateOnly.FromDateTime(DateTime.Now.AddDays(-30)),
            DateOnly.FromDateTime(DateTime.Now)
        );
        // Assert
        Assert.IsType<ActionResult<IEnumerable<ActivityDto>>>(result);
    }

    [Fact]
    public async Task GetFitnessChartData_CallsServiceMethods()
    {
        // Arrange
        _mockApiService
            .Setup(x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .ReturnsAsync(new List<ActivityDto> { new ActivityDto() });
        _mockMetricsService
            .Setup(x => x.TransformApiData(It.IsAny<List<ActivityDto>>()))
            .Returns(new List<ActivityDto> { new ActivityDto() });

        // Act
        var result = await _controller.GetFitnessChartData(
            "athleteId",
            DateOnly.FromDateTime(DateTime.Now.AddDays(-30)),
            DateOnly.FromDateTime(DateTime.Now)
        );

        // Assert
        _mockApiService.Verify(
            x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                ),
            Times.Once
        );
        _mockMetricsService.Verify(
            x => x.TransformApiData(It.IsAny<List<ActivityDto>>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetFitnessChartData_NoActivities_ReturnsNotFound()
    {
        // Arrange

        // Act
        var result = await _controller.GetFitnessChartData(
            "athleteId",
            DateOnly.FromDateTime(DateTime.Now.AddDays(-30)),
            DateOnly.FromDateTime(DateTime.Now)
        );

        // Assert
        _mockApiService.Verify(
            x =>
                x.LoadActivitiesAsync(
                    It.IsAny<string>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
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
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

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
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        // Act
        var result = await _controller.GetFitnessChartData(athleteId, startDate, endDate);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal("Invalid parameters provided.", badRequestResult.Value);
    }
}
