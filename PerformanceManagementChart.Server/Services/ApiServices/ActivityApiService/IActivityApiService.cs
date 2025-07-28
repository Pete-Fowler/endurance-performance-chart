using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public interface IActivityApiService
{
    /// <summary>
    /// Loads activities from the API by athlete ID and date range.
    /// </summary>
    /// <param name="athleteId">The ID of the athlete.</param>
    /// <param name="startDate">The start date for the activities.</param>
    /// <param name="endDate">The end date for the activities.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of activities.</returns
    Task<List<ActivityDto>> LoadActivitiesAsync(
        string athleteId,
        DateTime startDate,
        DateTime endDate
    );
}