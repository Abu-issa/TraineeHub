using TraineeHub.Web.ViewModels.Assignments;

namespace TraineeHub.Web.ViewModels.Topics
{
    public class TopicDetailsVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<TopicAssignmentItemVm> Assignments { get; set; } = new();
        public AssignmentCreateVm NewAssignment { get; set; } = new();
    }
}
