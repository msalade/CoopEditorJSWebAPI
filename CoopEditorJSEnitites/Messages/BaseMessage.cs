using CoopEditorJSEnitites.Enums;

namespace CoopEditorJSEnitites.Messages
{
	public class BaseMessage
	{
		public MessagesType Type { get; set; }
		public string OwnerId { get; set; }
	}
}
