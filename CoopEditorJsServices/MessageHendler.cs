using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices
{
	public class MessageHendler : IMessageHandler<BaseMessage>
	{
		private readonly IDispatcher _dispatcher;
		private readonly IRoomService _roomService;

		public MessageHendler(IDispatcher dispatcher, IRoomService roomService)
		{
			_dispatcher = dispatcher;
			_roomService = roomService;
		}

		public void Handle(BaseMessage message)
		{
			if (message != null)
			{
				switch (message.Type)
				{
					case MessagesType.Chat:
						_dispatcher.Invoke(() =>
						{

						});
						break;
					case MessagesType.Code:
					case MessagesType.Controll:
					case MessagesType.Error:
					case MessagesType.JHS:
					default:
						break;
				}
			}
		}
	}
}
