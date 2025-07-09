using System.Net.WebSockets;
using PerformanceManagementChart.Server.Models;

namespace PerformanceManagementChart.Server.Services;

public class IntervalsIcuApiService : ActivityApiService, IActivityApiService
{

    public IntervalsIcuApiService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }
    protected override string BaseUrl => "https://intervals.icu/api/v1/";

    public override async Task<List<ActivityDto>> LoadActivitiesAsync(int athleteId, DateOnly startDate, DateOnly endDate)
    {
        var url = GetUrl($"athlete/{athleteId}/activities?oldest={startDate}&newest={endDate}");

        // Use the HttpClientFactory to create a client and make the request in steps
        
        return await _httpClientFactory.CreateClient().GetFromJsonAsync<List<ActivityDto>>(url);
    }


}