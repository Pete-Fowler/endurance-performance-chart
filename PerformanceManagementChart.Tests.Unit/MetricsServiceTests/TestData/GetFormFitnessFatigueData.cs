using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Tests.MetricsService_Test;

public class GetFormFitnessFatigueData : IEnumerable<object[]>
{
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
                    Date = new DateTime(2023, 1, 1),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 110 },
                },
            },
            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    Fatigue = 100,
                    Fitness = 100,
                    Form = 0,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2),
                    Fatigue = 105,
                    Fitness = 105,
                    Form = 0,
                    Activity = new Activity { Load = 110 },
                },
            },
        };

        // 2 days, 1 day with multiple activities
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 8, 0, 0),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 10, 0, 0),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 50 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 110 },
                },
            },
            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 8, 0, 0),
                    Fatigue = 100,
                    Fitness = 100,
                    Form = 0,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 10, 0, 0),
                    Fatigue = 150,
                    Fitness = 150,
                    Form = 0,
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 50 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2),
                    Fatigue = 130,
                    Fitness = 130,
                    Form = 0,
                    Activity = new Activity { Load = 110 },
                },
            },
        };

        // 2 activities, with 1 zero load day in between
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 110 },
                },
            },
            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 100,
                    Fitness = 100,
                    Form = 0,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 50,
                    Fitness = 50,
                    Form = 0,
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 70,
                    Fitness = 70,
                    Form = 0,
                    Activity = new Activity { Load = 110 },
                },
            },
        };

        // 2 activities 8 days apart
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 110 },
                },
            },
            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 75,
                    Fitness = 75,
                    Form = 0,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 110,
                    Fitness = 92,
                    Form = -18,
                    Activity = new Activity { Load = 110 },
                },
            },
        };

        // 2 activities 43 days apart
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 2, 13, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 110 },
                },
            },
            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 75,
                    Fitness = 75,
                    Form = 0,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 2, 13, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 110,
                    Fitness = 110,
                    Form = 0,
                    Activity = new Activity { Load = 110 },
                },
            },
        };

        // 4 activities over 7 days, then 3 days with no activity
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 30 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 60 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 7, 0, 0, 0, DateTimeKind.Utc),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 70 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 0 },
                },
            },
            // Expected
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 100,
                    Fitness = 100,
                    Form = 0,
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 65,
                    Fitness = 65,
                    Form = 0,
                    Activity = new Activity { Load = 30 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 63,
                    Fitness = 63,
                    Form = 0,
                    Activity = new Activity { Load = 60 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 47,
                    Fitness = 47,
                    Form = 0,
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 38,
                    Fitness = 38,
                    Form = 0,
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 31,
                    Fitness = 31,
                    Form = 0,
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 7, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 37,
                    Fitness = 37,
                    Form = 0,
                    Activity = new Activity { Load = 70 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 22,
                    Fitness = 32,
                    Form = 10,
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 18,
                    Fitness = 28,
                    Form = 10,
                    Activity = new Activity { Load = 0 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                    Fatigue = 10,
                    Fitness = 26,
                    Form = 16,
                    Activity = new Activity { Load = 0 },
                },
            },
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
