using System;

namespace Bot
{
    public class IncorrectRoleException : Exception
    {
        public IncorrectRoleException(string message) : base(message)
        {
        }
    }
}