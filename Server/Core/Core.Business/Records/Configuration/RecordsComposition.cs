using Core.Business.Records.Component;
using Core.Business.Records.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Business.Records.Configuration
{
    public class RecordsComposition
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IRecordsComponent, RecordsComponent>();
            services.AddTransient<IRecordsComponentFacade, RecordsComponentFacade>();
        }
    }
}
