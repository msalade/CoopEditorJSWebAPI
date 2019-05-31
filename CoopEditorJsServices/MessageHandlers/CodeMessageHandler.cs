using System.Net.WebSockets;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class CodeMessageHandler : BaseMessageHandler<CodeMessage>
    {
        private readonly IWebSocketsService _webSocketsService;

        public CodeMessageHandler(IRoomService roomService, IWebSocketsService webSocketsService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
        }

        public bool Handle(CodeMessage message)
        {
            _roomService.UpdateRoomContent(message.Content, message.RoomId);
            var targetRoom = _roomService.GetRoom(message.RoomId);

            if (targetRoom?.UsersList != null)
            {
                _roomService.EnterRoom(message.User, message.RoomId);

                foreach (var user in targetRoom.UsersList)
                    if(user.WebSocket.State == WebSocketState.Open && user.Id != message.User?.Id)
                        _webSocketsService.SendMessage(message, user.WebSocket);
            }

            return true;
        }
    }
}
