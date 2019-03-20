using CoopEditorJsServices.Interfaces;
using CoopEditorJsServices.Middleware;
using CoopEditorJSWebAPI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoopEditorJSWebAPI
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			DependencyInjectionConfiguration.IntegrateSimpleInjector(services);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			DependencyInjectionConfiguration.InitializeContainer(app);
			loggerFactory.AddConsole(LogLevel.Debug);
			loggerFactory.AddDebug(LogLevel.Debug);

			app.UseWebSockets();
			app.Map("/editor", App => App.UseMiddleware<EditorWebSocketMiddleware>());

			app.UseMvc();
		}
	}
}
