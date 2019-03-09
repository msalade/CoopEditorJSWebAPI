using System.Net.WebSockets;
using Newtonsoft.Json;

namespace CoopEditorJSEnitites
{
	public class User
	{
		[JsonIgnore]
		public WebSocket WebSocket { get; set; }
		public string Nick { get; set; }
		public string Id { get; set; }
	}
}
