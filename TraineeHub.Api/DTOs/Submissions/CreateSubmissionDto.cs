using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Api.DTOs.Submissions
{
    public class CreateSubmissionDto
    {
        [Required]
        public Guid AssignmentId { get; set; }

        [Required]
        public Guid TraineerId { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; } = string.Empty;
    }
}
