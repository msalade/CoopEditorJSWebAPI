using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class ErrorMessageHandler : BaseMessageHandler<ErrorMessage>
	{
        public ErrorMessageHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(ErrorMessage message)
		{
			throw new System.NotImplementedException();
		}
    }
}
