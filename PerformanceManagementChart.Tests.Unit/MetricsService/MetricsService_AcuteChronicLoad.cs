using System.Text;
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
        Assert.Equal(expectedActivityData.Count, results.Count);

        Assert.Equivalent(expectedActivityData, results);
    }

    [Theory]
    [ClassData(typeof(GetFormFitnessFatigue))]
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
        Assert.Equal(expectedActivityData.Count, results.Count);

        for (int i = 0; i < expectedActivityData.Count; i++)
        {
            Assert.Equal(expectedActivityData[i].Date, results[i].Date);
            Assert.Equal(expectedActivityData[i].Fatigue, results[i].Fatigue);
            Assert.Equal(expectedActivityData[i].Fitness, results[i].Fitness);
            Assert.Equal(expectedActivityData[i].Form, results[i].Form);
        }
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
        Assert.Equal(expectedActivityData.Count, transformedActivities.Count);
        for (int i = 0; i < expectedActivityData.Count; i++)
        {
            Assert.Equal(expectedActivityData[i].Date, transformedActivities[i].Date);
            Assert.Equal(
                expectedActivityData[i].Activity.Load,
                transformedActivities[i].Activity.Load
            );
        }
    }
}
