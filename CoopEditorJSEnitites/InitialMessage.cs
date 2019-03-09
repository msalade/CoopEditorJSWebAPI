using System.Collections.Generic;

namespace CoopEditorJSEnitites
{
	public class InitialMessage
	{
		public Dictionary<string, User> Users { get; set; }
		public IEnumerable<Room> AvalibleRooms { get; set; }
	}
}
