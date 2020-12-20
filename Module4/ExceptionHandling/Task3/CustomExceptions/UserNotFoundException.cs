using System;

namespace Task3.CustomExceptions
{
    public class UserNotFoundException : AddUserTaskException
    {
        public override string Message { get; } = "User not found";
    }
}