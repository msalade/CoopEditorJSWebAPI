using CoopEditorJsServices;
using CoopEditorJsServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CoopEditorJSWebAPI.Configuration
{
	public class DependencyInjectionConfiguration
	{
		private Container _container { get; }

		public DependencyInjectionConfiguration()
		{
			_container = new Container();
		}

		public void IntegrateSimpleInjector(IServiceCollection services)
		{
			_container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			services.EnableSimpleInjectorCrossWiring(_container);
			services.UseSimpleInjectorAspNetRequestScoping(_container);
		}

		public void InitializeContainer(IApplicationBuilder app)
		{
			//register services
			_container.Register<IRoomService, RoomService>();
			_container.Register<IWebSocketsService, WebSocketsService>();
			_container.Register<IMessageService, MessageService>();

			_container.AutoCrossWireAspNetComponents(app);
		}
	}
}
