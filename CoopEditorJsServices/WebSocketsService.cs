using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoopEditorJsServices
{
	public class WebSocketsService : IWebSocketsService
	{
		public async Task<string> ExtractMessage(WebSocket socket, CancellationToken cancellationToken = default(CancellationToken))
		{
			var buffer = new ArraySegment<byte>(new byte[8192]);
			using (var memoryStream = new MemoryStream())
			{
				WebSocketReceiveResult result;
				do
				{
					cancellationToken.ThrowIfCancellationRequested();

					result = await socket.ReceiveAsync(buffer, cancellationToken);
					memoryStream.Write(buffer.Array, buffer.Offset, result.Count);
				}
				while (!result.EndOfMessage);

				memoryStream.Seek(0, SeekOrigin.Begin);
				if (result.MessageType != WebSocketMessageType.Text)
				{
					return null;
				}

				using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
				{
					return await reader.ReadToEndAsync();
				}
			}
		}

		public async void HandleMessage(string stringMessage, string socketId, Room room, CancellationToken cancellationToken = default(CancellationToken))
		{
			Message message = null;
			JObject jsonMessage = null;

			try
			{
				message = JsonConvert.DeserializeObject<Message>(stringMessage);
			}
			catch
			{

			}

			if (message != null)
			{
				await Task.WhenAll(room.UsersList.Select<User, Task>(user =>
				{
					try
					{
						if (user.WebSocket.State == WebSocketState.Open)
						{
							lock (user.WebSocket)
							{
								var buffer = Encoding.UTF8.GetBytes(stringMessage);
								var segment = new ArraySegment<byte>(buffer);
								Task sendTask = user.WebSocket.SendAsync(segment, WebSocketMessageType.Text, true, cancellationToken);
								sendTask.Wait();
							}
						}
					}
					catch (Exception ex)
					{

					}

					return Task.CompletedTask;
				}).ToArray());
			}
		}
	}
}
