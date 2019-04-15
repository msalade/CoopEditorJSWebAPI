using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJsServices.Interfaces;

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

		public void SendMessage(string message, WebSocket socket, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (message != null && socket != null)
			{
				try
				{
					lock (socket)
					{
						var segmentedMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
						Task sendTask = socket.SendAsync(segmentedMessage, WebSocketMessageType.Text, true, cancellationToken);
						sendTask.Wait();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}
	}
}
