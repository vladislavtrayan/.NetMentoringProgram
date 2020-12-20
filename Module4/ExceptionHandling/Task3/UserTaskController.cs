using System;
using Task3.CustomExceptions;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            try
            {
                _taskService.AddTaskForUser(userId, new UserTask(description));
            }
            catch (AddUserTaskException e)
            {
                model.AddAttribute("action_result", e.Message);
                return false;
            }
            return true;
        }
    }
}