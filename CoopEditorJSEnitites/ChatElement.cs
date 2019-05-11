using System;

namespace CoopEditorJSEnitites
{
    public class ChatElement
    {
        public string Content { get; set; }
        public DateTime CreationDate => DateTime.Now;
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
