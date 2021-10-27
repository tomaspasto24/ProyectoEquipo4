namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class NoCommandCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool VerifyCondition(Message condition)
        {
            return true;
        }
    }
}