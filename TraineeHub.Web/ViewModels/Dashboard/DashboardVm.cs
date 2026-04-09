namespace TraineeHub.Web.ViewModels.Dashboard
{
    public class DashboardVm
    {
        public int TraineesCount { get; set; }
        public int TopicsCount { get; set; }
        public int AssignmentsCount { get; set; }

        public int PendingSubmissionsCount { get; set; }
        public int SubmittedSubmissionsCount { get; set; }
        public int ApprovedSubmissionsCount { get; set; }
        public int RejectedSubmissionsCount { get; set; }

        public bool FromCache { get; set; }
    }
}