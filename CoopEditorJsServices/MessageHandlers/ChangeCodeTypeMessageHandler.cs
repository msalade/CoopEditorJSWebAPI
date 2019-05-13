using System;
using System.Net.WebSockets;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    public class ChangeCodeTypeMessageHandler : BaseMessageHandler<ControlMessage>
    {
        private readonly IWebSocketsService _webSocketsService;

        public ChangeCodeTypeMessageHandler(IRoomService roomService, IWebSocketsService webSocketsService) : base(roomService)
        {
            _webSocketsService = webSocketsService;
        }

        public bool Handle(ControlMessage message)
        {
            if (message.CommandType == CommandsTypes.ChangeCodeType)
            {
                var targetRoom = _roomService.GetRoom(message.RoomId);

                if (targetRoom != null)
                {
                    object languageType;
                    Enum.TryParse(typeof(LanguagesTypes), message.Content, out languageType);
                    targetRoom.TypeCode = (LanguagesTypes)languageType;

                    if (targetRoom.UsersList != null)
                        foreach (var user in targetRoom.UsersList)
                        {
                            if (user.WebSocket.State == WebSocketState.Open)
                                _webSocketsService.SendMessage(new ControlMessage
                                {
                                    Content = message.Content,
                                    CommandType = CommandsTypes.ChangeCodeType
                                }, user.WebSocket);
                        }
                }

                return true;
            }

            return false;
        }
    }
}
