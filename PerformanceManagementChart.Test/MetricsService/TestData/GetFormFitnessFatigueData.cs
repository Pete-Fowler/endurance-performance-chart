using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Test.MetricsService;

public class GetFormFitnessFatigue : IEnumerable<object[]>
{
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

        // 2 activities 8 days apart
        yield return new object[]
        {
            // Input
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 9),
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
                    Fatigue = 75,
                    Fitness = 75,
                    Form = 0,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 9),
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
                    Date = new DateTime(2023, 1, 1),
                    ThreshholdPace = 8.33,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 2, 13),
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
                    Fatigue = 75,
                    Fitness = 75,
                    Form = 0,
                    Activity = new Activity { Load = 75 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 2, 13),
                    Fatigue = 110,
                    Fitness = 110,
                    Form = 0,
                    Activity = new Activity { Load = 110 },
                },
            },
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
