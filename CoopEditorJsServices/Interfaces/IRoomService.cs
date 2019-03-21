using System.Net.WebSockets;

namespace CoopEditorJsServices.Interfaces
{
	public interface IRoomService
	{
		void AddNewUser(string id, WebSocket socket);
	}
}
