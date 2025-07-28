using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Tests.MetricsService_Test;

public class AddNonActivityDaysData : IEnumerable<object[]>
{
    // Input, endDate, expected output
    public IEnumerator<object[]> GetEnumerator()
    {
        // One gap of zero activities
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 200 },
                },
            },

            new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),

            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc) },
                new ActivityDto { Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc) },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 200 },
                },
            },
        };

        // Two gaps of zero activities
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 150 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 250 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 250 },
                },
            },

            new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc),

            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 150 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc), Activity = null },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 250 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc), Activity = null },
                new ActivityDto { Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc), Activity = null },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 250 },
                },
            },
        };

        // 1 gap, multiple activities on same day
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 10, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 300 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 400 },
                },
            },

            new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc),

            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 10, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 300 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc) },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc   ),
                    Activity = new Activity { Load = 400 },
                },
            },
        };

        // 2 activities, end date 2 days after last activity
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 200 },
                },
            },
            new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc),
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc) },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc) },
                new ActivityDto { Date = new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Utc) },
            },
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
