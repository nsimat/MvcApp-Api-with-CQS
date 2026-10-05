namespace WebbApiwithCQS.Domain.DTOs
{
    public class TaskToUpdate
    {
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool? IsClosed { get; set; }
    }
}
