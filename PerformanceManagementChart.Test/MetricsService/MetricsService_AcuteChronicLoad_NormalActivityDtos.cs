using System.Collections;
using System.Text;
using PerformanceManagementChart.Server.Models;
using static PerformanceManagementChart.Server.Services.MetricsService;

namespace PerformanceManagementChart.Test.MetricsService;

public class NormalActivityDtos : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<ActivityDto>
            {
                new ActivityDto { Date = new DateTime(2023, 1, 1), Activity = new Activity { Load = 100 } },
                new ActivityDto { Date = new DateTime(2023, 1, 4), Activity = new Activity { Load = 200 } },
            },
            new List<ActivityDto>
            {
                new ActivityDto { Date = new DateTime(2023, 1, 1), Activity = new Activity { Load = 100 } },
                new ActivityDto { Date = new DateTime(2023, 1, 2),  },
                new ActivityDto { Date = new DateTime(2023, 1, 3),  },
                new ActivityDto { Date = new DateTime(2023, 1, 4), Activity = new Activity { Load = 250 } },
            },
        };

        // yield return new object[]
        // {
        //     new List<ActivityDto>
        //     {
        //         new ActivityDto { Date = new DateTime(2023, 1, 1), Activity = new Activity { Load = 150 } },
        //         new ActivityDto { Date = new DateTime(2023, 1, 3), Activity = new Activity { Load = 250 } },
        //     },
        //     new List<ActivityDto>
        //     {
        //         new ActivityDto { Date = new DateTime(2023, 1, 1) },
        //         new ActivityDto { Date = new DateTime(2023, 1, 2) },
        //     },
        // };
    }

    /*
 public class ActivityDto
    {
        public DateTime Date { get; set; }
        public int Fatigue { get; set; }
        public int Fitness { get; set; }
        public int Form { get; set; }

        /// <summary>
        /// Miles / hour
        /// </summary>
        public double ThreshholdPace { get; set; } = 8.33;
        public Activity? Activity { get; set; }
    }

    public class Activity
    {
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Miles
        /// </summary>
        public double Distance { get; set; }
        public int Load { get; set; }
        public double Intensity { get; set; }
        public int AvgHeartRate { get; set; }

        /// <summary>
        /// Miles / hour
        /// </summary>
        public double AvgPace { get; set; }

        /// <summary>
        /// Miles / hour
        /// </summary>
        public double GradeAdjustedPace { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Time { get; set; }
    }
    */

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
