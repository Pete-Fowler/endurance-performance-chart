using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using PerformanceManagementChart.Server.Controllers;

namespace PerformanceManagementChart.Tests.Controllers;

public class FitnessChartControllerTests
{
    private ControllerBase _controller;

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

    // test that a call to GetFitnessChartData with no athleteId, startDate and endDate args 
    // uses defaults to call the IntervalsIcuApiService for a List<activityDto> 
    // then 
    // [Fact]
    // public void FitnessChartController_HasGetFitnessChartDataRoute()
    // {
    //     // Act
    //     var action = _controller.GetType().GetMethod("GetFitnessChartData");

    //     // Assert
    //     Assert.NotNull(action);
    //     Assert.Equal("GetFitnessChartData", action.Name);
    //     Assert.Equal(
    //         "HttpGetAttribute",
    //         action.GetCustomAttributes(typeof(HttpGetAttribute), false)[0].GetType().Name
    //     );
    //     Assert.Equal(
    //         "api/fitnesschart",
    //         action.GetCustomAttributes(typeof(RouteAttribute), false)[0].GetType().Name
    //     );
    // }
}
