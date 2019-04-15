namespace CoopEditorJsServices.Interfaces
{
	public interface IMessageService
	{
		dynamic DeserializeMessage(dynamic message);
		string ParseMessage(dynamic message);
	}
}
