using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    class JoinRoomHandler : BaseMessageHandler<ControlMessage>
    {
        private readonly IWebSocketsService _webSocketsService;

        public JoinRoomHandler(IRoomService roomService, IWebSocketsService webSocketsService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
        }

        public bool Handle(ControlMessage message)
        {
            if (message.CommandType == CommandsTypes.JoinToRoom)
            {
                _roomService.EnterRoom(message.User, message.RoomId);
                var targetRoom = _roomService.GetRoom(message.RoomId);

                _webSocketsService.SendMessage(new CodeMessage
                {
                    Content = targetRoom.EditorContent,
                    LanguageType = targetRoom.TypeCode
                }, message.User.WebSocket);

                return true;
            }

            return false;
        }
    }
}
