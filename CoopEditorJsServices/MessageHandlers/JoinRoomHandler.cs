using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    class JoinRoomHandler : BaseMessageHandler<ControllMessage>
    {
        public JoinRoomHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(ControllMessage message)
        {
            if (message.CommandType == CommandsTypes.JoinToRoom)
            {
                _roomService.EnterRoom(message.User, message.RoomId);
                return true;
            }

            return false;
        }
    }
}
