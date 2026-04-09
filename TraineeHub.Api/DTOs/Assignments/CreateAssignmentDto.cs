using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Api.DTOs.Assignments
{
    public class CreateAssignmentDto
    {
        [Required]
        public Guid TopicTd { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 5)]
        public int Difficulty { get; set; }

        [Required]
        public DateTime DutDate { get; set; }
    }
}
