using System.Net.WebSockets;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices.Interfaces
{
	public interface IRoomService
	{
		void AddNewUser(WebSocket socket);
		void RemoveUser(string id, string roomId);
		User GetUser(string id, string roomId, bool isPublicRoom = false);
		void EnterRoom(User user, string roomId);
		string CreateRoom(User user, string roomName = "");
	}
}
