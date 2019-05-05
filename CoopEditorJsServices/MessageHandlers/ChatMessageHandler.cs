using System.Net.WebSockets;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class ChatMessageHandler : BaseMessageHandler<ChatMessage>
    {
        private readonly IWebSocketsService _webSocketsService;

        public ChatMessageHandler(IRoomService roomService, IWebSocketsService webSocketsService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
        }

        public bool Handle(ChatMessage message)
        {
            _roomService.SendChatMessage(message?.Content, message?.RoomId);
            var usersList = _roomService.GetRoom(message?.RoomId)?.UsersList;
            if (usersList != null)
                foreach (var user in usersList)
                    if(user.WebSocket.State == WebSocketState.Open)
                        _webSocketsService.SendMessage(message, user.WebSocket);

            return true;
        }
    }
}
