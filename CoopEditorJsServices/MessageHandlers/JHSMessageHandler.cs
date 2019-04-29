using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices.MessageHandlers
{
	public class JHSMessageHandler : BaseMessageHandler<JHSMessage>
	{
        public JHSMessageHandler(IRoomService roomService) : base(roomService) { }

        public bool Handle(JHSMessage message)
        {

            return true;
        }
    }
}
