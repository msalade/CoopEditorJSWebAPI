using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class CodeMessageHandler : BaseMessageHandler<CodeMessage>
	{
        public CodeMessageHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(CodeMessage message)
		{
			throw new System.NotImplementedException();
		}
    }
}
