using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices
{
	public class RoomService : IRoomService
	{
		private readonly Room _globalRoom;
		private readonly HashSet<Room> _privateRooms;

		public RoomService()
		{
			_globalRoom = new Room("Global room");
			_globalRoom.UsersList = new HashSet<User>();
			_privateRooms = new HashSet<Room>();
		}

		public void AddNewUser(WebSocket socket)
		{
			_globalRoom.UsersList?.Add(new User(socket));
		}

		public void RemoveUser(string id, string roomId)
		{
			_privateRooms.FirstOrDefault(room => room.Id == roomId)
				?.UsersList?.RemoveWhere(user => user.Id == id);
		}

		public User GetUser(string id, string roomId, bool isPublicRoom = false)
		{
			if (isPublicRoom)
				return _globalRoom.UsersList.FirstOrDefault(user => user.Id == id);

			return _privateRooms.FirstOrDefault(room => room.Id == roomId)
				?.UsersList?.FirstOrDefault(user => user.Id == id);
		}

		public void EnterRoom(User user, string roomId)
		{
			_privateRooms.FirstOrDefault(room => room.Id == roomId)
				?.UsersList?.Add(user);
		}

		public string CreateRoom(User user, string roomName = "")
		{
			var newRoom = new Room(roomName);

			newRoom.UsersList.Add(user);
			_privateRooms.Add(newRoom);

			return newRoom.Id;
		}
	}
}
