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
        DateOnly startDate,
        DateOnly endDate
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
            JsonSerializer.Deserialize<List<ActivityDto>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new List<ActivityDto>();

        return activities;
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
