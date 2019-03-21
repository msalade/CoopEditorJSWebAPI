using System.Collections.Generic;
using System.Net.WebSockets;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices
{
	public class RoomService : IRoomService
	{
		private readonly Room _globalRoom;
		private HashSet<Room> _privateRooms;

		public RoomService()
		{
			_globalRoom = new Room("Global room");
			_globalRoom.UsersList = new HashSet<User>();
			_privateRooms = new HashSet<Room>();
		}

		public void AddNewUser(string id, WebSocket socket)
		{
			throw new System.NotImplementedException();
		}
	}
}
