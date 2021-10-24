namespace Bot
{
    public interface ICondition
    {
        bool VerifyCondition(Message condition);
    }
}