namespace TraineeHub.Web.ViewModels.Topics
{
    public class TopicListItemVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AssignmentCount { get; set; }
    }
}
