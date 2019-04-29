using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    class CreateRoomHandler : BaseMessageHandler<ControllMessage>
    {
        public CreateRoomHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(ControllMessage message)
        {
            if (message.CommandType == CommandsTypes.CreateRoom)
            {
                _roomService.CreateRoom(message.User, message.Content);
            }

            return false;
        }
    }
}
