using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.Interfaces
{
	public interface IMessageService
	{
		BaseMessage ParseMessage(string message);
	}
}
