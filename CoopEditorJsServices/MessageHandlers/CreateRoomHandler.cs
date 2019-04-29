using System;
using System.Collections.Generic;
using System.Text;
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

            }

            return false;
        }
    }
}
