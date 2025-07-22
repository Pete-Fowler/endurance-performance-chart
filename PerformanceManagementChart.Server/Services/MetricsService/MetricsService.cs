using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public class MetricsService : IMetricsService
{
    /// <summary>
    /// Transforms API data into a format suitable for performance management charts.
    /// </summary>
    /// <param name="activities">The list of activities.</param>
    /// <returns>A list of transformed activity data.</returns>
    public List<ActivityDto> TransformApiData(List<ActivityDto> activities)
    {
        if (activities == null || activities.Count == 0)
        {
            return new List<ActivityDto>();
        }

        foreach (var activity in activities)
        {
            if (activity.Activity != null)
            {
                activity.Activity.Load = GetLoad(activity);
            }
        }

        List<ActivityDto> activitiesWithNonActivityDays = AddNonActivityDays(activities);

        List<ActivityDto> transformedActivities = GetFormFitnessFatigue(
            activitiesWithNonActivityDays
        );

        return transformedActivities;
    }

    ///
    /// <summary>
    /// Calculates the Load / Training Stress Score (TSS) for a single activity.
    /// </summary>
    /// <param name="activityData">The activity data.</param>
    /// <returns>The calculated TSS.</returns>
    public int GetLoad(ActivityDto activityData)
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
    public List<ActivityDto> AddNonActivityDays(List<ActivityDto> activities)
    {
        if (activities == null || activities.Count == 0)
        {
            return new List<ActivityDto>();
        }

        DateTime endDate = activities[0].Date;
        DateTime startDate = activities[activities.Count - 1].Date;

        List<ActivityDto> newActivityData = new List<ActivityDto>();

        Dictionary<DateTime, List<ActivityDto>> activityDict = activities
            .GroupBy(a => a.Date.Date)
            .ToDictionary(g => g.Key, g => g.ToList());

        for (DateTime date = endDate.Date; date >= startDate.Date; date = date.AddDays(-1))
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

    /// <summary>
    /// Calculates the Form, Fitness, and Fatigue metrics for a list of activities.
    /// </summary>
    /// <param name="activities">The list of activities.</param>
    /// <returns>A new list of activities with Form, Fitness, and Fatigue calculated.</returns>
    public List<ActivityDto> GetFormFitnessFatigue(List<ActivityDto> activities)
    {
        if (activities == null || activities.Count == 0)
        {
            return new List<ActivityDto>();
        }

        // Calculate the 7-day and 42-day rolling averages for Fatigue, Fitness, and Form
        var result = new List<ActivityDto>();

        for (int i = 0; i < activities.Count; i++)
        {
            var current = activities[i];
            var sevenDayAvg = GetRollingAverage(activities, i, 7);
            var fortyTwoDayAvg = GetRollingAverage(activities, i, 42);

            result.Add(
                new ActivityDto
                {
                    Date = current.Date,
                    Fatigue = (int)sevenDayAvg,
                    Fitness = (int)fortyTwoDayAvg,
                    Form = (int)fortyTwoDayAvg - (int)sevenDayAvg,
                    ThreshholdPace = current.ThreshholdPace,
                    Activity = current.Activity,
                }
            );
        }

        return result;
    }

    /// <summary>
    /// Gets the rolling average of activity loads over a specified number of days.
    /// </summary>
    /// <param name="activities">The list of activities.</param>
    /// <param name="index">The index of the activity to start the rolling average from.</param>
    /// <param name="days">The number of days to include in the rolling average.</param>
    /// <returns>The average load over the specified number of days.</returns>
    private static double GetRollingAverage(List<ActivityDto> activities, int index, int days)
    {
        DateTime endDate = activities[index].Date.Date;
        DateTime startDate = endDate.AddDays(-(days - 1));

        // Group activities by date and sum loads for each day
        var relevantActivities = activities
            .Take(index + 1)
            .Where(a => a.Date.Date >= startDate && a.Date.Date <= endDate)
            .GroupBy(a => a.Date.Date)
            .ToDictionary(g => g.Key, g => g.Sum(a => a.Activity?.Load ?? 0));

        if (!relevantActivities.Any())
        {
            return 0;
        }

        return relevantActivities.Values.Average();
    }
}
