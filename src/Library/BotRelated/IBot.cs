namespace Bot
{
    public interface IBot
    {
        void StartCommunication();
        void SendMessage(string id, string text);
        void HandleMessage(Message text);
    }
}