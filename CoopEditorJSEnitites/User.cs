using System;
using System.Net.WebSockets;
using Newtonsoft.Json;

namespace CoopEditorJSEnitites
{
	public class User
	{
		public User() {}

		public User(WebSocket socket)
		{
			WebSocket = socket;
			Id = Guid.NewGuid().ToString();
		}

		[JsonIgnore]
		public WebSocket WebSocket { get; set; }
		public string Nick { get; set; }
		public string Id { get; set; }
	}
}
