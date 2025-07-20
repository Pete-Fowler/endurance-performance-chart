namespace PerformanceManagementChart.Server.Models
{
    public class IntervalsIcuActivity
    {
        public string id { get; set; } = "";
        public string type { get; set; } = "";

        /// <summary>
        /// Seconds
        /// </summary>
        public int elapsed_time { get; set; }
        public string name { get; set; } = "";
        public string? description { get; set; }
        public DateTime start_date { get; set; }

        /// <summary>
        /// Meters
        /// </summary>
        public double? distance { get; set; }

        /// <summary>
        /// Seconds
        /// </summary>
        public double moving_time { get; set; }

        /// <summary>
        /// Meters / second
        /// </summary>
        public double? average_speed { get; set; }

        public int? average_heartrate { get; set; }

        /// <summary>
        /// Grade adjusted pace, m/s
        /// </summary>
        public double? gap { get; set; }

        /// <summary>
        /// meters / sec
        /// </summary>
        public double? threshhold_pace { get; set; }
    }
}
