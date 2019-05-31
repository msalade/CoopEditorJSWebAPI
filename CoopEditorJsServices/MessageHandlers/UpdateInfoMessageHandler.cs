using System;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    public class UpdateInfoMessageHandler : BaseMessageHandler<ControlMessage>
    {
        private readonly IWebSocketsService _webSocketsService;
        private readonly IMessageService _messageService;

        public UpdateInfoMessageHandler(IRoomService roomService, IWebSocketsService webSocketsService,
            IMessageService messageService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
            _messageService = messageService;
        }

        public bool Handle(ControlMessage message)
        {
            if (message.CommandType == CommandsTypes.UpdateInformation)
            {
                _webSocketsService.SendMessage(new ControlMessage
                {
                    Content = _messageService.ParseMessage(new UserInfo
                    {
                        RoomId = message.RoomId,
                        Rooms = _roomService.GetAllRooms(),
                        UserId = message.User.Id
                    }),
                    CommandType = CommandsTypes.UpdateInformation,
                    User = null
                }, message.User.WebSocket);

                return true;
            }

            return false;
        }
    }
}
