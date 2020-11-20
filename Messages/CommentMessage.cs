using System;
namespace Messages
{
    public class CommentMessage : Message
    {
        public string Name { get; set; }

        public CommentMessage(string name, string text) : base(text)
        {
            Name = name;
        }
    }
}
