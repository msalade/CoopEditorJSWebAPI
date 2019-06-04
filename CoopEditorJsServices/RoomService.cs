using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;

namespace CoopEditorJsServices
{
	public class RoomService : IRoomService
	{
        private readonly HashSet<Room> _privateRooms;

        public RoomService()
		{
			_privateRooms = new HashSet<Room>();
        }

		public void RemoveUser(string id, string roomId)
		{
			_privateRooms.FirstOrDefault(room => room.Id == roomId)
				?.UsersList?.RemoveWhere(user => user.Id == id);
		}

		public User GetUser(string id, string roomId)
		{
			return _privateRooms.FirstOrDefault(room => room.Id == roomId)
				?.UsersList?.FirstOrDefault(user => user.Id == id);
		}

		public void EnterRoom(User user, string roomId)
		{
            Task.Run(() =>
            {
                var usersList = _privateRooms.FirstOrDefault(room => room.Id == roomId)?.UsersList;
                var existingUser = usersList?.FirstOrDefault(x => x.Id == user.Id);
                if (existingUser == null)
                {
                    usersList?.Add(user);
                }
                else
                {
                    existingUser.WebSocket = user.WebSocket;
                }
            });
        }

		public string CreateRoom(User user, string roomName = "")
		{
			var newRoom = new Room(roomName);

			newRoom.UsersList.Add(user);
			_privateRooms.Add(newRoom);

			return newRoom.Id;
		}

        public void RemoveRoom(string roomId)
        {
            var roomToRemove = _privateRooms?.FirstOrDefault(room => room?.Id == roomId);

            if (roomToRemove != null)
                _privateRooms.Remove(roomToRemove);
        }

        public void UpdateRoomContent(string content, string roomId)
        {
            Task.Run(() =>
            {
                var targetRoom = _privateRooms.FirstOrDefault(room => room.Id == roomId);
                if (targetRoom != null)
                {
                    targetRoom.EditorContent = content;
                }
            });
        }

        public void SendChatMessage(ChatElement chatMessage, string roomId)
        {
            Task.Run(() =>
            {
                _privateRooms.FirstOrDefault(room => room.Id == roomId)?.ChatList?.Add(chatMessage);          
            });
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _privateRooms;
        }

        public Room GetRoom(string id)
        {
            return _privateRooms.FirstOrDefault(room => room.Id == id);
        }
    }
}
