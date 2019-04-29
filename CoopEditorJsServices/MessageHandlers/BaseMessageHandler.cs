using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
    public abstract class BaseMessageHandler<T> : IMessageHandler<T> where T : BaseMessage
    {
        protected readonly IRoomService _roomService;

        protected BaseMessageHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public bool Handle(T message)
        {
            return true;
        }
    }
}
