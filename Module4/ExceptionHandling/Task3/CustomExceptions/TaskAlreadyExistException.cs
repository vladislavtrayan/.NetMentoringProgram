using System;

namespace Task3.CustomExceptions
{
    public class TaskAlreadyExistException : AddUserTaskException
    {
        public override string Message { get; } = "The task already exists";
    }
}