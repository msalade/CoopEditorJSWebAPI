using CoopEditorJSEnitites.Enums;

namespace CoopEditorJSEnitites.Messages
{
	public class ErrorMessage : BaseMessage
	{
		public ErrorMessage(string content)
		{
			Content = content;
		}

		public string Content { get; set; }
		public MessagesType Type => MessagesType.Error;
	}
}
