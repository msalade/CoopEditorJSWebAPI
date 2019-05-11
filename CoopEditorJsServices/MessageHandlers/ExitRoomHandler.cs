using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    class ExitRoomHandler : BaseMessageHandler<ControllMessage>
    {
        public ExitRoomHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(ControllMessage message)
        {
            if (message.CommandType == CommandsTypes.ExitRoom)
            {
                _roomService.RemoveUser(message.User.Id, message.RoomId);
                return true;
            }

            return false;
        }
    }
}
