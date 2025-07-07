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

    /// <summary>
    /// Adds non-activity days to the list of activities.
    /// </summary>
    /// <param name="activities">The list of activities. Must be sorted by date ascending</param>
    /// <returns>A new list of activities with non-activity days added.</returns>
    public static List<ActivityDto> AddNonActivityDays(List<ActivityDto> activities)
    {
        if (activities == null || activities.Count == 0)
        {
            return new List<ActivityDto>();
        }

        DateTime startDate = activities[0].Date;
        DateTime endDate = activities[activities.Count - 1].Date;

        List<ActivityDto> newActivityData = new List<ActivityDto>();

        Dictionary<DateTime, List<ActivityDto>> activityDict = activities
            .GroupBy(a => a.Date.Date)
            .ToDictionary(g => g.Key, g => g.ToList());

        for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
        {
            if (!activityDict.TryGetValue(date, out List<ActivityDto>? activitiesOnDate))
            {
                newActivityData.Add(new ActivityDto { Date = date, Activity = null });
            }
            else
            {
                newActivityData.AddRange(activitiesOnDate);
            }
        }

        return newActivityData;
    }
}
