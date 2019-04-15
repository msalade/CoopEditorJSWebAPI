using CoopEditorJsServices.Interfaces;
using SimpleInjector;

namespace CoopEditorJsServices
{
	public class MessageProcessor : IMessageProcessor
	{
		private readonly Container _container;

		public MessageProcessor(Container container)
		{
			_container = container;
		}

		public void ProcessMessage(dynamic message)
		{
			var type = typeof(IMessageHandler<>).MakeGenericType(message.GetType());
			var handler = _container.GetAllInstances(type);
			foreach (var messageHandler in handler)
			{
				if (messageHandler.Handle(message))
				{
					break;
				}
			}
		}
	}
}
