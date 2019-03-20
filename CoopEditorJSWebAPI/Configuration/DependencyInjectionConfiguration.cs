using CoopEditorJsServices;
using CoopEditorJsServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CoopEditorJSWebAPI.Configuration
{
	public static class DependencyInjectionConfiguration
	{
		static Container container = new Container();

		public static void IntegrateSimpleInjector(IServiceCollection services)
		{
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			services.EnableSimpleInjectorCrossWiring(container);
			services.UseSimpleInjectorAspNetRequestScoping(container);
		}

		public static void InitializeContainer(IApplicationBuilder app)
		{
			//register services
			container.Register<IRoomService, RoomService>();
			container.Register<IWebSocketsService, WebSocketsService>();
			container.Register<IMessageService, MessageService>();

			container.AutoCrossWireAspNetComponents(app);
		}

		public static Container GetContainer()
		{
			return container;
		}
	}
}
