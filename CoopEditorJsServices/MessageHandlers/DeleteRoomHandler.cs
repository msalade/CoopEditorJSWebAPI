using System.Net.WebSockets;
using CoopEditorJsServices.Interfaces;
using CoopEditorJsServices.MessageHandlers;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoOpEditor.Services.MessageHandlers
{
    public class DeleteRoomHandler : BaseMessageHandler<ControlMessage>
    {
        private readonly IWebSocketsService _webSocketsService;

        public DeleteRoomHandler(IRoomService roomService, IWebSocketsService webSocketsService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
        }

        public bool Handle(ControlMessage message)
        {
            if (message.CommandType == CommandsTypes.DeleteRoom)
            {
                var targetRoom = _roomService.GetRoom(message.RoomId);
                foreach (var user in targetRoom.UsersList)
                    if (user.WebSocket.State == WebSocketState.Open)
                        _webSocketsService.SendMessage(message, user.WebSocket);

                _roomService.RemoveRoom(message.RoomId);
                return true;
            }

            return false;
        }
    }
}
