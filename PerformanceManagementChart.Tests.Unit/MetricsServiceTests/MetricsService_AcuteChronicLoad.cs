using System.Text;
using FluentAssertions;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;
using PerformanceManagementChart.Tests.MetricsService_Test.TestData;

// using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Tests.MetricsService_Test;

public class MetricsService_AcuteChronicLoad
{
    // This will test a method called AddNonActivityDays, which will take a list of
    // ActivityDto, and add one record for each day that does not have an activity.
    // For those added days, the activity will be null and the load will be zero.

    [Theory]
    [ClassData(typeof(AddNonActivityDaysData))]
    public void AddNonActivityDays_NormalData_ReturnsExpected(
        List<ActivityDto> activities,
        List<ActivityDto> expectedActivityData
    )
    {
        // Arrange
        var svc = new MetricsService();

        // Act
        var results = svc.AddNonActivityDays(activities);

        // Assert
        results
            .Should()
            .BeEquivalentTo(expectedActivityData, options => options.WithStrictOrdering());
    }

    [Theory]
    [ClassData(typeof(GetFormFitnessFatigueData))]
    public void GetFormFitnessFatigue_NormalData_ReturnsExpected(
        List<ActivityDto> activities,
        List<ActivityDto> expectedActivityData
    )
    {
        // Arrange
        var svc = new MetricsService();

        // Act
        var results = svc.GetFormFitnessFatigue(activities);

        // Assert
        results
            .Should()
            .BeEquivalentTo(expectedActivityData, options => options.WithStrictOrdering());
    }

    [Theory]
    [ClassData(typeof(TransformApiDataData))]
    public void TransformApiData_AddsNonactivityDaysLoadAndFormFitnessFatigue(
        List<ActivityDto> activityData,
        List<ActivityDto> expectedActivityData
    )
    {
        // Arrange
        var svc = new MetricsService();

        // Act
        var transformedActivities = svc.TransformApiData(activityData);

        // Assert
        transformedActivities
            .Should()
            .BeEquivalentTo(expectedActivityData, options => options.WithStrictOrdering());
    }
}
