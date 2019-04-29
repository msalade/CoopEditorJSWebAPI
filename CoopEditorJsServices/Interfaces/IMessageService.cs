namespace CoopEditorJsServices.Interfaces
{
	public interface IMessageService
	{
		dynamic DeserializeMessage(string message);
		string ParseMessage(dynamic message);
	}
}
