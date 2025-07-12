using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using PerformanceManagementChart.Server.Controllers;
using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Tests.Controllers;

public class FitnessChartControllerTests
{
    private readonly FitnessChartController _controller;

    public FitnessChartControllerTests()
    {
        NullLogger logger = NullLogger.Instance;
        _controller = new FitnessChartController(logger);
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
    public void FitnessChartController_ReturnsActionResult()
    {
        // Arrange
        // Act
        var result = _controller.GetFitnessChartData();
        // Assert
        Assert.IsType<ActionResult<IEnumerable<ActivityDto>>>(result);
    }

    // Test that it calls LoadActivitiesAsync, then calls AddNonActivityDays
    // then calls 

}
