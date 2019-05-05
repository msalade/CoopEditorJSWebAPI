using System.Collections.Generic;
using System.Net.WebSockets;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices.Interfaces
{
	public interface IRoomService
	{
		void RemoveUser(string id, string roomId);
		User GetUser(string id, string roomId);
		void EnterRoom(User user, string roomId);
		string CreateRoom(User user, string roomName = "");
        void UpdateRoomContent(string content, string roomId);
        void SendChatMessage(ChatElement chatMessage, string roomId);
        IEnumerable<Room> GetAllRooms();
        Room GetRoom(string id);
    }
}
