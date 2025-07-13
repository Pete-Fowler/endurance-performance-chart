using System;

namespace PerformanceManagementChart.Server.Services.ApiServices.ActivityApiServiceFactory;

public interface IActivityApiServiceFactory
{
    public IActivityApiService GetActivityApiService(string serviceType);

}
