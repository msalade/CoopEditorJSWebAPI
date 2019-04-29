using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites
{
	public class Room
	{
		public Room(string name, bool isPublic = true)
		{
			Name = name;
			IsPublic = isPublic;
			Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public HashSet<User> UsersList { get; set; }
		public bool IsPublic { get; set; }
        public string EditorContent { get; set; }
        public List<ChatElement> ChatList { get; set; } = new List<ChatElement>();
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeCode TypeCode { get; set; }
	}
}
