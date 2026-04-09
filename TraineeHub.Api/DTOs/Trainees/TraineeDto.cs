namespace TraineeHub.Api.DTOs.Trainees
{
    public class TraineeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; }

    }
}
