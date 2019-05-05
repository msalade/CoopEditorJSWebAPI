using System.Collections.Generic;

namespace CoopEditorJSEnitites
{
    public class UserInfo
    {
        public string RoomId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public string UserId { get; set; }
    }
}
