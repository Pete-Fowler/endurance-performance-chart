using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Tests.MetricsService_Test;

public class AddNonActivityDaysData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        // One gap of zero activities
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    Activity = new Activity { Load = 100 },
                },
            },
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 3) },
                new ActivityDto { Date = new DateTime(2023, 1, 2) },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    Activity = new Activity { Load = 100 },
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
                    Date = new DateTime(2023, 1, 6),
                    Activity = new Activity { Load = 250 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3),
                    Activity = new Activity { Load = 250 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    Activity = new Activity { Load = 150 },
                },
            },
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 6),
                    Activity = new Activity { Load = 250 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 5), Activity = null },
                new ActivityDto { Date = new DateTime(2023, 1, 4), Activity = null },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 3),
                    Activity = new Activity { Load = 250 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 2), Activity = null },
                new ActivityDto { Date = new DateTime(2023, 1, 1), Activity = new Activity { Load = 150 } },
            },
        };

        // 1 gap, multiple activities on same day
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 10, 0, 0),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 12, 0, 0),
                    Activity = new Activity { Load = 300 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4),
                    Activity = new Activity { Load = 400 },
                },
            },
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 1),
                    Activity = new Activity { Load = 100 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 10, 0, 0),
                    Activity = new Activity { Load = 200 },
                },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 2, 12, 0, 0),
                    Activity = new Activity { Load = 300 },
                },
                new ActivityDto { Date = new DateTime(2023, 1, 3) },
                new ActivityDto
                {
                    Date = new DateTime(2023, 1, 4),
                    Activity = new Activity { Load = 400 },
                },
            },
        };
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
