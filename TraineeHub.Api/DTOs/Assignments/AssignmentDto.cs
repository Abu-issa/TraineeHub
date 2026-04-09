namespace TraineeHub.Api.DTOs.Assignments
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public Guid TopicTd { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public DateTime DutDate { get; set; }
    }
}
