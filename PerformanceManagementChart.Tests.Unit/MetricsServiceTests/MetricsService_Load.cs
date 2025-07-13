using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;

namespace PerformanceManagementChart.Tests.MetricsService_Test;

public class MetricsServiceTests
{
    [Theory]
    [MemberData(nameof(LoadNormalTestCases))]
    public void GetLoad_NormalData_ReturnsExpected(ActivityDto activity, int expectedLoad)
    {
        // Arrange
        var service = new MetricsService();

        // Act
        var load = service.GetLoad(activity);

        // Assert
        Assert.Equal(expectedLoad, load);
        // IF = 7.1 / 8.33 = .85
        // TSS = (14115 * 7.1 * .85) / (8.33 * 3600) * 100 = 284.060...
    }

    [Theory]
    [MemberData(nameof(LoadCasesThatShouldReturnZero))]
    public void GetLoad_NoActivity_ReturnsZero(ActivityDto activity, int expectedLoad)
    {
        // Arrange
        var service = new MetricsService();

        // Act
        var load = service.GetLoad(activity);

        // Assert   
        Assert.Equal(load, expectedLoad);
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

    public static IEnumerable<object[]> LoadCasesThatShouldReturnZero =>
        new List<object[]>
        {
            new object[]
            {
                new ActivityDto
                {
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Duration = 0, GradeAdjustedPace = 8.33 },
                },
                0,
            },
            new object[]
            {
                new ActivityDto
                {
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Duration = 3600, GradeAdjustedPace = 0 },
                },
                0,
            },
            new object[]
            {
                new ActivityDto { ThreshholdPace = 8.33, Activity = null },
                0,
            },
        };
}
