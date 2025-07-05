
namespace PerformanceManagementChart.Server.Models
{
    public class ActivityDto
    {
        public DateTime Date { get; set; }
        public int Fatigue { get; set; }
        public int Fitness { get; set; }
        public int Form { get; set; }

        /// <summary>
        /// Miles / hour
        /// </summary>
        public double ThreshholdPace { get; set; } = 8.41;
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
}