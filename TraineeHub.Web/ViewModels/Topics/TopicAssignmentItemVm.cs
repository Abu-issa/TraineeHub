namespace TraineeHub.Web.ViewModels.Topics
{
    public class TopicAssignmentItemVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public DateTime DueDate { get; set; }
    }
}
