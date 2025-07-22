using System;
using System.Collections;
using Microsoft.VisualBasic;
using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Tests.MetricsServiceTests.TestData;

public class IntervalsIcuApiServiceTestData : IEnumerable<object[]>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// api response json, expected activity dto list
    /// </summary>
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            File.ReadAllText("IntervalsIcuApiServiceTests/TestData/ApiResults.json"),
            new List<ActivityDto>
            {
                new ActivityDto
                {
                    Date = new DateTime(2018, 4, 15),
                    ThreshholdPace = 8.33,
                    Activity = new Activity
                    {
                        Type = "Run",
                        Duration = 1815,
                        Distance = 3.11,
                        AvgHeartRate = 125,
                        AvgPace = 6.17,
                        GradeAdjustedPace = 6.31,
                        Name = "Brookline Running",
                        Time = new DateTime(2018, 4, 15, 13, 8, 43, DateTimeKind.Utc)
                    }
                },
                new ActivityDto
                {
                    Date = new DateTime(2018, 4, 16),
                    ThreshholdPace = 8.33,
                    Activity = new Activity
                    {
                        Type = "Run",
                        Duration = 14115,
                        Distance = 26.46,
                        AvgHeartRate = 129,
                        AvgPace = 6.75,
                        GradeAdjustedPace = 7.01,
                        Name = "Hopkinton Running",
                        Time = new DateTime(2018, 4, 16, 14, 56, 18, DateTimeKind.Utc)
                    }
                },
            },
        };
    }
}
