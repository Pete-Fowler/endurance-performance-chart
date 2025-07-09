using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public interface IMetricsService
{
    /// <summary>
    /// Calculates the Load / Training Stress Score (TSS) for a single activity.
    /// </summary>
    /// <param name="activityData">The activity data.</param>
    /// <returns>The calculated TSS.</returns>
    int GetLoad(ActivityDto activityData);

    /// <summary>
    /// Adds non-activity days to the list of activities.
    /// </summary>
    /// <param name="activities">The list of activities.</param>
    /// <returns>A new list of activities with non-activity days added.</returns>
    List<ActivityDto> AddNonActivityDays(List<ActivityDto> activities);

    /// <summary>
    /// Calculates the Form, Fitness, and Fatigue metrics for a list of activities.
    /// </summary>
    /// <param name="activities">The list of activities.</param>
    /// <returns>A new list of activities with Form, Fitness, and Fatigue calculated.</returns>
    List<ActivityDto> GetFormFitnessFatigue(List<ActivityDto> activities);
}
