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
                    Date = new DateTime(2018, 4, 16),
                    ThreshholdPace = 8.33,
                    Activity = new Activity
                    {
                        Type = "Run",
                        Duration = 14115,
                        Distance = 26.4591,
                        AvgHeartRate = 129,
                        AvgPace = 6.748847,
                        GradeAdjustedPace = 7.0099467485160005,
                        Name = "Hopkinton Running",
                        Time = new DateTime(2018, 4, 16, 14, 56, 18, DateTimeKind.Utc)
                    }
                },
                // make another from the second activity in ApiResults.json
                new ActivityDto
                {
                    Date = new DateTime(2018, 4, 15),
                    Fatigue = 80,
                    Fitness = 180,
                    Form = 40,
                    ThreshholdPace = 8.33,
                    Activity = new Activity
                    {
                        Type = "Run",
                        Duration = 3600,
                        Distance = 3.1,
                        Load = 120,
                        Intensity = 0.75,
                        AvgHeartRate = 125,
                        AvgPace = 6.16,
                        GradeAdjustedPace = 6.31,
                        Name = "Brookline Running - CHANGE THIS",
                        Time = new DateTime(2018, 4, 15, 13, 8, 43, DateTimeKind.Utc)
                    }
                }
            },
        };
    }
}
