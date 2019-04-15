using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.MessageHandlers
{
	public class ErrorMessageHandler : IMessageHandler<ErrorMessage>
	{
		public bool Handle(ErrorMessage message)
		{
			throw new System.NotImplementedException();
		}
	}
}
