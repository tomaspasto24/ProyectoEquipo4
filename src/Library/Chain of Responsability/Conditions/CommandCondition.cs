namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool VerifyCondition(Message condition)
        {
            return condition.Text == "/comandos";
        }
    }
}