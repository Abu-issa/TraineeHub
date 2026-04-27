using TraineeHub.Domain.Enum;

namespace TraineeHub.Web.ViewModels.Submissions
{
    public class SubmissionExportVm
    {
        public string TraineeFullName { get; set; }
        public string AssignmentTitle { get; set; }
        public string Notes { get; set; }
        public SubmissionStatus Stauts { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string ReviewAction { get; set; }
    }
}
