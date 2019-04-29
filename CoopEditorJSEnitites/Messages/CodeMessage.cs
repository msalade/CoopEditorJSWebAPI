using CoopEditorJSEnitites.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites.Messages
{
	public class CodeMessage : BaseMessage
	{
		public string Content { get; set; } = "";
		[JsonConverter(typeof(StringEnumConverter))]
		public LanguagesTypes LanguageType { get; set; }
	}
}
