using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public class IntervalsIcuApiService : ActivityApiService, IActivityApiService
{
    public IntervalsIcuApiService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory) { }

    protected override string BaseUrl => "https://intervals.icu/api/v1/";

    public override async Task<List<ActivityDto>> LoadActivitiesAsync(
        string athleteId,
        DateTime startDate,
        DateTime endDate
    )
    {
        var url = GetUrl(
            $"athlete/{athleteId}/activities?oldest={startDate.ToString("yyyy-MM-dd")}&newest={endDate.ToString("yyyy-MM-dd")}"
        );

        var client = GetClient();
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error fetching activities: {response.ReasonPhrase}");
        }
        var content = await response.Content.ReadAsStringAsync();
        var activities =
            JsonSerializer.Deserialize<List<IntervalsIcuActivity>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new List<IntervalsIcuActivity>();

        var activityDtos = activities
            .Select(activity => new ActivityDto
            {
                Date = activity.start_date.Date,
                ThreshholdPace =
                    activity.threshhold_pace != null ? (double)activity.threshhold_pace * 2.23694 : 8.33,
                Activity = new Activity
                {
                    Type = activity.type,
                    Duration = activity.elapsed_time,
                    Distance = Math.Round((activity.distance ?? 0) / 1609.344, 2),
                    AvgHeartRate = activity.average_heartrate ?? 0,
                    AvgPace = Math.Round((activity.average_speed ?? 0) * 2.23694, 2),
                    GradeAdjustedPace = Math.Round((activity.gap ?? 0) * 2.23694, 2),
                    Name = activity.name,
                    Time = activity.start_date,
                },
            })
            .ToList();

        if (activityDtos.Count > 1 && activityDtos[0].Date > activityDtos[^1].Date)
        {
            activityDtos = activityDtos.OrderBy(a => a.Date).ToList();
        }

        return activityDtos;
    }

    private HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient();

        var apiKey = Environment.GetEnvironmentVariable("INTERVALS_ICU_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException(
                "API key for Intervals.icu is not set in environment variables."
            );
        }

        var authString = $"API_KEY:{apiKey}";
        var base64AuthString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64AuthString);

        return client;
    }
}
