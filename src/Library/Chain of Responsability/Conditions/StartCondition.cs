namespace Bot
{
    public class StartCondition : ICondition
    {
        public bool VerifyCondition(Message condition)
        {
            return condition.Text == "hola";
        }
    }
}