using CoopEditorJSEnitites.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites.Messages
{
	public class ErrorMessage : BaseMessage
	{
		public ErrorMessage(string content)
		{
			Content = content;
            User = new User();
		}

		public string Content { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MessagesType Type => MessagesType.Error;
	}
}
