namespace TraineeHub.Web.ViewModels.Trainees
{
    public class TraineeListItemVm
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; }
        public int SubmissionCount { get; set; }
    }
}
