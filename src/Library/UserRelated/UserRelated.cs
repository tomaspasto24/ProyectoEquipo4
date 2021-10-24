namespace Bot
{
    public enum State
    {
        Start, Chatting
    }
    public class UserRelated
    {
        public State State {get; set;}
        public IBot Channel {get; set;}
        public User User {get; set;}
        public UserRelated()
        {
            this.State = State.Start;
            this.Channel = null;
            this.User = null;
        }
    }
}