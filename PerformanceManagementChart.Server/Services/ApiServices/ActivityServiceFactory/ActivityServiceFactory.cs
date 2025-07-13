using System;

namespace PerformanceManagementChart.Server.Services.ApiServices.ActivityApiServiceFactory;

public class ActivityApiServiceFactory
{
    private readonly IntervalsIcuApiService _intervalsApiService;

    public ActivityApiServiceFactory(IntervalsIcuApiService intervalsApiService)
    {
        _intervalsApiService = intervalsApiService;
    }

    /// <summary>
    /// A factory method registered in the dependency injection container to get the 
    /// correct type of activity api service. This would come from user info in a 
    /// full app, but for this demo, "intervals" will be hardcoded as a default.
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public IActivityApiService GetActivityApiService(string serviceType)
    {
        return serviceType switch
        {
            "intervals" => _intervalsApiService,
            _ => throw new ArgumentException("Unknown activity api service type."),
        };
    }
}
