using CoopEditorJSEnitites.Enums;
using CoopEditorJSEnitites.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites
{
	public class JHSMessage : BaseMessage
	{
		public Content Content { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LanguagesTypes LanguagesTypes => LanguagesTypes.JSC;
		[JsonConverter(typeof(StringEnumConverter))]
		public MessagesType Type => MessagesType.JHS;
	}

	public class Content
	{
		public string CssCode { get; set; } = "";
		public string HtmlCode { get; set; } = "";
		public string JsCode { get; set; } = "";
	}
}
