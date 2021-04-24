using Core.Business.Records.Component;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Business.Records.Configuration
{
    public class RecordsRegistrations
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IRecordsComponent, RecordsComponent>();
        }
    }
}
