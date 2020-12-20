using System;

namespace Task3.CustomExceptions
{
    public class InvalidAddUserIdException : AddUserTaskException
    {
        public override string Message { get; } = "Invalid userId";

        public InvalidAddUserIdException(string userId)
        {
            Message += $" userId is {userId}";
        }

        public InvalidAddUserIdException()
        {
        }
    }
}