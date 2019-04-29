using CoopEditorJSEnitites.Enums;

namespace CoopEditorJSEnitites.Messages
{
	public class BaseMessage
	{
		public MessagesType Type { get; set; }
		public User User { get; set; }
        public string RoomId { get; set; }
	}
}
