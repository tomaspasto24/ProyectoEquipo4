namespace Bot
{
    public class Message
    {
        public Message(string id, string message)
        {
            this.UserId = id;
            this.Text = message;
        }
        public string UserId {get; set;}
        public string Text {get; set;}
    }
}