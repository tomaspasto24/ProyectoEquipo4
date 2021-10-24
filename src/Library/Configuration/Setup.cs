namespace Bot
{
    public class Setup
    {
        public static AbstractHandler HandlerSetup()
        {
            AbstractHandler start = new StartHandler(new StartCondition());
            AbstractHandler noCommand = new NoCommandHandler(new NoCommandCondition());

            start.Succesor = noCommand;
            
            return start;
        }
    }
}