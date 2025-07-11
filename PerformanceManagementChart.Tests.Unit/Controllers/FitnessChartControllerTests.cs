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
}
