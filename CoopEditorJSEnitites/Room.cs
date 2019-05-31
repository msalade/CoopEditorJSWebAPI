using System;
using System.Collections.Generic;
using CoopEditorJSEnitites.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoopEditorJSEnitites
{
	public class Room
	{
		public Room(string name)
		{
			Name = name;
			Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public HashSet<User> UsersList { get; set; } = new HashSet<User>();
        public string EditorContent { get; set; }
        public List<ChatElement> ChatList { get; set; } = new List<ChatElement>();

        [JsonConverter(typeof(StringEnumConverter))]
        public LanguagesTypes TypeCode { get; set; } = LanguagesTypes.javascript;
    }
}
