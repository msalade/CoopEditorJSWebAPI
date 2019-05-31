using CoopEditorJsServices;
using CoopEditorJsServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CoOpEditor.WebAPI.Configuration
{
	public static class DependencyInjectionConfiguration
	{
		static readonly Container container = new Container();

		public static void IntegrateSimpleInjector(IServiceCollection services)
		{
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			services.EnableSimpleInjectorCrossWiring(container);
			services.UseSimpleInjectorAspNetRequestScoping(container);
		}

		public static void InitializeContainer(IApplicationBuilder app)
		{
			//register singleton
			container.RegisterSingleton<IRoomService, RoomService>();

			//register services
			container.Register<IWebSocketsService, WebSocketsService>();
			container.Register<IMessageService, MessageService>();
			container.Register<IMessageProcessor>(() => new MessageProcessor(container));
			container.AutoCrossWireAspNetComponents(app);

            //register handlers
            container.RegisterCollection(typeof(IMessageHandler<>), typeof(IMessageHandler<>).Assembly);
        }

		public static Container GetContainer()
		{
			return container;
		}
	}
}
