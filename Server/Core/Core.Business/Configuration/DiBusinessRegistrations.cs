using Core.Business.Auth;
using Core.Business.Auth.Component;
using Core.Business.Auth.Initializer;
using Core.Business.Files.Component;
using Core.Business.Files.Component.Models;
using Core.Business.Records.Configuration;
using Core.DataAccess.Composition;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Business.Configuration
{
    public class DiBusinessRegistrations
    {
        public void Register(IServiceCollection services)
        {
            RegisterDataAccess(services);

            RegisterRecords(services);
            RegisterFiles(services);
            RegisterAuth(services);
        }

        private void RegisterAuth(IServiceCollection services)
        {
            services.AddTransient<AuthInitializer>();

            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped(
                x => (IMutableCurrentUser)x.GetService<ICurrentUser>());

            services.AddTransient<IAuthComponent, AuthComponent>();
            services.AddTransient<IUserComponent, UserComponent>();
            services.AddTransient<UserManager>();
        }

        private void RegisterDataAccess(IServiceCollection services)
        {
            var registrator = new DataAcessRegistrations();
            registrator.Register(services);
        }

        private void RegisterRecords(IServiceCollection services)
        {
            var registrator = new RecordsComposition();
            registrator.Register(services);
        }

        private void RegisterFiles(IServiceCollection services)
        {
            services.AddTransient<IFilesComponent, FilesComponent>();
            services.AddTransient<IFileStoreFactory, FileStoreFactory>();
        }
    }
}
