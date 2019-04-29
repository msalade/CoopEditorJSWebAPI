using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class ChatMessageHandler : BaseMessageHandler<ChatMessage>
    {
        public ChatMessageHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(ChatMessage message)
        {
            //TODO add method to send message to chat members

            return true;
        }
    }
}
