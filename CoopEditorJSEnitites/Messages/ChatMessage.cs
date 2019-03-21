using CoopEditorJSEnitites.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites.Messages
{
	public class ChatMessage : BaseMessage
	{
		public string Content { get; set; }
		public User Sender { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public MessagesType Type => MessagesType.Chat;
	}
}
