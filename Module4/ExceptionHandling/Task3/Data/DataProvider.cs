using System;
using System.Collections.Generic;
using System.Text;
using Task3.CustomExceptions;

namespace Task3.Data
{
    public static class DataProvider
    {
        private static Dictionary<Type, string> Messages { get; set; }

        static DataProvider()
        {
            Messages = new Dictionary<Type, string>();
            Messages.Add(typeof(UserNotFoundException), "User not found");
            Messages.Add(typeof(TaskAlreadyExistException), "The task already exists");
            Messages.Add(typeof(InvalidAddUserIdException), "Invalid userId");
        }
        public static string GetErrorMessage(AddUserTaskException exception)
        {
            return Messages.GetValueOrDefault(exception.GetType());
        }
    }
}
