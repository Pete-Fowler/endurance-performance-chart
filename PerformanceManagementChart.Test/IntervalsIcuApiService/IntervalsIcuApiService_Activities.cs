using System.Text;
using Moq;
using Moq.Protected;
using PerformanceManagementChart.Server.Models;
using PerformanceManagementChart.Server.Services;

namespace PerformanceManagementChart.Test.IntervalsIcuApiServiceTests;

public class IntervalsIcuApiService_Activities
{

    private readonly IntervalsIcuApiService _service;

    public IntervalsIcuApiService_Activities()
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage());

        var mockFactory = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient(mockHandler.Object);
        mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _service = new IntervalsIcuApiService(mockFactory.Object);
    }

    [Theory]
    [InlineData(
        "athlete/12345/activities?oldest=2018-01-01&newest=2018-04-16",
        "https://intervals.icu/api/v1/athlete/12345/activities?oldest=2018-01-01&newest=2018-04-16"
    )]
    [InlineData("athlete/6789/activities", "https://intervals.icu/api/v1/athlete/6789/activities")]
    public void GetUrl_ReturnsExpected(string inputUrl, string expectedUrl)
    {
        var result = _service.GetUrl(inputUrl);

        Assert.Equal(expectedUrl, result);
    }
}
