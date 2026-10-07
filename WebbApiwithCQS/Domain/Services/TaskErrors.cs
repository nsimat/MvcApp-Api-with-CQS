using WebbApiwithCQS.Domain.Abstractions.Errors;

namespace WebbApiwithCQS.Domain.Services
{
    public static class TaskErrors
    {
        public static Error NotFound => new Error("Task.Errors", "Task not found!");
        public static Error AlreadyTerminated => new Error("Task.Errors", "Task already terminated!");
        public static Error NotInserted => new Error("Task.Errors", "Task not inserted!");
    }
}
