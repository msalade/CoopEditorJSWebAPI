using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class ErrorMessageHandler : BaseMessageHandler<ErrorMessage>
    {
        private readonly IWebSocketsService _webSocketsService;
        private readonly IMessageService _messageService;

        public ErrorMessageHandler(IRoomService roomService, IWebSocketsService webSocketsService, IMessageService messageService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
            _messageService = messageService;
        }

        public bool Handle(ErrorMessage message)
        {
            var stringMessage = _messageService.ParseMessage(message);
			_webSocketsService.SendMessage(stringMessage, message.User.WebSocket);

            return true;
        }
    }
}
