using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Provider.Api.Web.DotnetCore.Tests
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
            _startup = new Startup(configuration);
        }

        public IConfiguration Configuration { get; }
        private Startup _startup;

        public void ConfigureServices(IServiceCollection services)
        {
            _startup.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ProviderStateMiddleware>();
            //app.Use(typeof(AuthorizationTokenReplacementMiddleware),
            //    app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1"));

            _startup.Configure(app, env);
        }
    }
}