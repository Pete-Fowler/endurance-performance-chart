using System;
using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Tests.MetricsService_Test.TestData;

public class TransformApiDataData : IEnumerable<object[]>
{
    // Params: activityData, endDate, expected output

    // Test has no zero days between activity days for simplicity.
    // Don't call AddNonActivityDays in the test to keep the tests independent
    public IEnumerator<object[]> GetEnumerator()
    {
        // 2 day consecutive average
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { GradeAdjustedPace = 8, Duration = 2500 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { GradeAdjustedPace = 7.5, Duration = 3600 },
                },
            },
    
            new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),

            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 64,
                    Fitness = 64,
                    Form = 0,
                    Activity = new Activity
                    {
                        GradeAdjustedPace = 8,
                        Duration = 2500,
                        Load = 64,
                        Intensity = 96,
                    },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 72,
                    Fitness = 72,
                    Form = 0,
                    Activity = new Activity
                    {
                        GradeAdjustedPace = 7.5,
                        Duration = 3600,
                        Load = 81,
                        Intensity = 90,
                    },
                },
            },
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
