using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Test.MetricsService;

public class GetFormFitnessFatigue : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        // I need some simple examples to test 7 day and 42 day average loads

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
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
