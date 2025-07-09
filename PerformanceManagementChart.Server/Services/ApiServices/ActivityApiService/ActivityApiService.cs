using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public abstract class ActivityApiService
{
    protected IHttpClientFactory _httpClientFactory;

    protected ActivityApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Gets the base URL for the API.
    /// </summary>
    protected abstract string BaseUrl { get; }

    /// <summary>
    /// Gets the full URL for a specific endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to append to the base URL.</param>
    /// <returns>The full URL as a string.</returns>
    public string GetUrl(string endpoint)
    {
        return $"{BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
    }

    /// <summary>
    /// Loads activities from the API by athlete ID and date range.
    /// </summary>
    /// <param name="athleteId">The ID of the athlete.</param>
    /// <param name="startDate">The start date for the activities.</param>
    /// <param name="endDate">The end date for the activities.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of activities.</returns>
    public abstract Task<List<ActivityDto>> LoadActivitiesAsync(
        int athleteId,
        DateOnly startDate,
        DateOnly endDate
    );
}
