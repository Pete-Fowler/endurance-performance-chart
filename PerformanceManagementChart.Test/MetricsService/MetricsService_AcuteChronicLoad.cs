using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Test.MetricsService;

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
        var results = AddNonActivityDays(activities);

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
        var results = GetFormFitnessFatigue(activities);

        Assert.Equal(expectedActivityData.Count, results.Count);

        for (int i = 0; i < expectedActivityData.Count; i++)
        {
            Assert.Equal(expectedActivityData[i].Date, results[i].Date);
            Assert.Equal(expectedActivityData[i].Fatigue, results[i].Fatigue);
            Assert.Equal(expectedActivityData[i].Fitness, results[i].Fitness);
            Assert.Equal(expectedActivityData[i].Form, results[i].Form);
        }
        // Assert.Equivalent(expectedActivityData, results);
    }
}
