using System.Net.WebSockets;
using System.Text.Json;
using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public class IntervalsIcuApiService : ActivityApiService, IActivityApiService
{
    public IntervalsIcuApiService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory) { }

    protected override string BaseUrl => "https://intervals.icu/api/v1/";

    public override async Task<List<ActivityDto>> LoadActivitiesAsync(
        int athleteId,
        DateOnly startDate,
        DateOnly endDate
    )
    {
        var url = GetUrl($"athlete/{athleteId}/activities?oldest={startDate}&newest={endDate}");

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
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
        );
        client.DefaultRequestHeaders.Add("User-Agent", "PerformanceManagementChart.Client");
        client.DefaultRequestHeaders.Add("X-Api-Key", "YOUR_API_KEY_HERE"); // Replace with your actual API key
        return client;
    }
}
