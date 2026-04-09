using TraineeHub.Domain.Enum;

namespace TraineeHub.Web.ViewModels.Submissions
{
    public class SubmissionListItemVm
    {
        public Guid Id { get; set; }

        public Guid AssignmentId { get; set; }
        public string AssignmentTitle { get; set; } = string.Empty;

        public Guid TraineerId { get; set; }
        public string TraineeFullName { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public SubmissionStatus Stauts { get; set; } = SubmissionStatus.Pending;

        public DateTime SubmittedAt { get; set; }
    }
}
