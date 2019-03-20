using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices.Interfaces
{
	public interface IWebSocketsService
	{
		Task<string> ExtractMessage(WebSocket socket, CancellationToken cancellationToken = default(CancellationToken));
		void HandleMessage(string stringMessage, string socketId, Room room, CancellationToken cancellationToken = default(CancellationToken));
		void SendMessage(string message, WebSocket socket, CancellationToken cancellationToken = default(CancellationToken));
	}
}
