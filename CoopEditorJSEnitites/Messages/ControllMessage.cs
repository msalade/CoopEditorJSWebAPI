using CoopEditorJSEnitites.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites.Messages
{
	public class ControllMessage : BaseMessage
	{
		public string Content { get; set; } = "";
		[JsonConverter(typeof(StringEnumConverter))]
		public CommandsTypes CommandType { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public MessagesType Type => MessagesType.Controll;
	}
}
