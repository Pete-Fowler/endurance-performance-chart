using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Test.MetricsService;

public class MetricsService_Load
{
    [Theory]
    [MemberData(nameof(LoadNormalTestCases))]
    public void GetLoad_NormalData_ReturnsExpected(ActivityDto activity, int expectedLoad)
    {
        // Act
        var load = GetLoad(activity);

        // Assert
        Assert.Equal(expectedLoad, load);
        // IF = 7.1 / 8.33 = .85
        // TSS = (14115 * 7.1 * .85) / (8.33 * 3600) * 100 = 284.060...
    }

    public static IEnumerable<object[]> LoadNormalTestCases =>
        new List<object[]>
        {
            new object[]
            {
                new ActivityDto
                {
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Duration = 3001, GradeAdjustedPace = 6.5 },
                },
                51,
            },
            new object[]
            {
                new ActivityDto
                {
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Duration = 14115, GradeAdjustedPace = 7.1 },
                },
                285,
            },
            new object[]
            {
                new ActivityDto
                {
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Duration = 3600, GradeAdjustedPace = 8.33 },
                },
                100,
            },
            new object[]
            {
                new ActivityDto
                {
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Duration = 7200, GradeAdjustedPace = 8.33 },
                },
                200,
            },
        };
}
