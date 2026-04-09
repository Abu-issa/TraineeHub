namespace TraineeHub.Api.DTOs.Submissions
{
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid TraineerId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string Stauts { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }
}
