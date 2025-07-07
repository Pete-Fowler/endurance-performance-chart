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
    [ClassData(typeof(NormalActivityDtos))]
    public void AddNonActivityDays_NormalData_ReturnsExpected(
        List<ActivityDto> activities,
        List<ActivityDto> expectedActivityData
    )
    {
        var results = MetricsService.AddNonActivityDays(activities);

        Assert.Equal(expectedActivityData.Count, results.Count);

        for (int i = 0; i < expectedActivityData.Count; i++)
        {
            Assert.True(false);
            Assert.Equivalent(expectedActivityData[i], results[i]);
        }
    }
}
