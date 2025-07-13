using System;

namespace PerformanceManagementChart.Server.Services.ApiServices.ActivityServiceFactory;

public interface IActivityServiceFactory
{
    public IActivityApiService GetActivityApiService(string serviceType);

}
