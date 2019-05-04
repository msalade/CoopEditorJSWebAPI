using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices.Interfaces
{
	public interface IWebSocketsService
	{
		Task<string> ExtractMessage(WebSocket socket, CancellationToken cancellationToken = default(CancellationToken));
		void SendMessage(dynamic message, WebSocket socket, CancellationToken cancellationToken = default(CancellationToken));
	}
}
