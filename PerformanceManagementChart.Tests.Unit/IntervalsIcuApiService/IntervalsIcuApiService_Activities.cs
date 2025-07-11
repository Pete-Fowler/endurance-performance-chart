using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;

namespace PerformanceManagementChart.Test.IntervalsIcuApiServiceTests;

public class IntervalsIcuApiService_Activities
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Mock<HttpMessageHandler> _mockHandler;

    public IntervalsIcuApiService_Activities()
    {
        DotNetEnv.Env.Load();

        _mockHandler = new Mock<HttpMessageHandler>();
        _mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage());

        var mockFactory = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient(_mockHandler.Object);
        mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);


        _httpClientFactory = mockFactory.Object;
    }

    [Theory]
    [InlineData(
        "athlete/12345/activities?oldest=2018-01-01&newest=2018-04-16",
        "https://intervals.icu/api/v1/athlete/12345/activities?oldest=2018-01-01&newest=2018-04-16"
    )]
    [InlineData("athlete/6789/activities", "https://intervals.icu/api/v1/athlete/6789/activities")]
    public void GetUrl_ReturnsExpected(string inputUrl, string expectedUrl)
    {
        var apiService = new IntervalsIcuApiService(_httpClientFactory);

        var result = apiService.GetUrl(inputUrl);

        Assert.Equal(expectedUrl, result);
    }

    // Now test that the load method it makes an http request to the expected url 
    [Theory]
    [InlineData(

        "https://intervals.icu/api/v1/athlete/12345/activities?oldest=2018-01-01&newest=2018-04-16"
    )]
    public async Task LoadActivitiesAsync(
        string expectedUrl
    )
    {
        // Arrange
        var activityDtos = new List<ActivityDto>
        {
            new ActivityDto { },
            new ActivityDto { }
        };

        _mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(activityDtos), Encoding.UTF8, "application/json")
            });

        var apiService = new IntervalsIcuApiService(_httpClientFactory);
        var athleteId = 12345;
        var startDate = new DateOnly(2018, 1, 1);
        var endDate = new DateOnly(2018, 4, 16);

        // Act
        var activities = await apiService.LoadActivitiesAsync(athleteId, startDate, endDate);

        // Assert
        _mockHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString() == expectedUrl
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        Assert.NotNull(activities);
        Assert.IsType<List<ActivityDto>>(activities);
    }
}
