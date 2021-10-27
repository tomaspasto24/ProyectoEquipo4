using System;


namespace Bot
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        static void Main(string[] args)
        {
            Utils u = new Utils();
            u.ComprobarCi("54332615");

            CodeGenerator generator = new CodeGenerator();
            generator.Generator();
        }

    }
}
