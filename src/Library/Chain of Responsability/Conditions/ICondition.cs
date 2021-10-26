namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        bool VerifyCondition(Message condition);
    }
}