using Core.DataAccess.Base.Database;
using Core.DataAccess.Records.DB;
using Core.DataAccess.Records.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DataAccess.Composition
{
    public class DataAcessRegistrations
    {
        public void Register(IServiceCollection services)
        {
            RegisterRecords(services);
            RegisterFiles(services);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void RegisterRecords(IServiceCollection services)
        {
            services.AddTransient<IRecordsRepository, RecordsRepository>();
        }

        private void RegisterFiles(IServiceCollection services)
        {
            services.AddTransient<IFileStore, LocalFileStore>();
        }
    }
}
