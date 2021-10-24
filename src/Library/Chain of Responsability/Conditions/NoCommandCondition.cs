namespace Bot
{
    public class NoCommandCondition : ICondition
    {
        public bool VerifyCondition(Message condition)
        {
            return true;
        }
    }
}