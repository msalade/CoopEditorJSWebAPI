using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class ErrorMessageHandler : BaseMessageHandler<ErrorMessage>
    {
        private readonly IWebSocketsService _webSocketsService;

        public ErrorMessageHandler(IRoomService roomService, IWebSocketsService webSocketsService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
        }

        public bool Handle(ErrorMessage message)
        {
			_webSocketsService.SendMessage(message, message.User.WebSocket);

            return true;
        }
    }
}
