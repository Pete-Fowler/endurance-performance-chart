using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public static class MetricsService
{
    /// <summary>
    /// Calculates the Training Stress Score (TSS) based on the activity data.
    /// </summary>
    /// <param name="activityData">The activity data.</param>
    /// <returns>The calculated TSS.</returns>
    public static int GetLoad(ActivityDto activityData)
    {
        Activity? activity = activityData.Activity;
        if (activity == null)
        {
            return 0;
        }

        double gap = activity.GradeAdjustedPace;
        double threshholdPace = activityData.ThreshholdPace;

        double intensityFactor = gap / threshholdPace;

        double tss = (activity.Duration * gap * intensityFactor) / (threshholdPace * 3600) * 100;

        return (int)Math.Round(tss);
    }
}

