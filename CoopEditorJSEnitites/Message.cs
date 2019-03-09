using CoopEditorJSEnitites.Enums;

namespace CoopEditorJSEnitites
{
	public class Message
	{
		public string CssCode { get; set; } = "";
		public string HtmlCode { get; set; } = "";
		public string JsCode { get; set; } = "";
		public MessagesType Type { get; set; } = MessagesType.UpdateCode;
	}
}
