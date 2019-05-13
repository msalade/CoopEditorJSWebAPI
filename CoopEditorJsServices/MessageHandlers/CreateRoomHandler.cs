using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    class CreateRoomHandler : BaseMessageHandler<ControlMessage>
    {
        private readonly IWebSocketsService _webSocketsService;
        private readonly IMessageService _messageService;

        public CreateRoomHandler(IRoomService roomService, IWebSocketsService webSocketsService,
            IMessageService messageService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
            _messageService = messageService;
        }

        public bool Handle(ControlMessage message)
        {
            if (message.CommandType == CommandsTypes.CreateRoom)
            {
                var romId = _roomService.CreateRoom(message.User, message.Content);

                _webSocketsService.SendMessage(new ControlMessage
                {
                    Content = _messageService.ParseMessage(new UserInfo { RoomId = romId, Rooms = _roomService.GetAllRooms() }),
                    CommandType = CommandsTypes.UpdateInformation,
                    User = null
                }, message.User.WebSocket);
            }

            return false;
        }
    }
}
