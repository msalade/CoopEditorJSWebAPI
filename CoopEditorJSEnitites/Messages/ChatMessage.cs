using CoopEditorJSEnitites.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites.Messages
{
	public class ChatMessage : BaseMessage
	{
		public ChatElement Content { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public MessagesType Type => MessagesType.Chat;
	}
}
