using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Api.DTOs.Trainees
{
    public class CreateTraineeDto
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(63)]
        public string Email { get; set; } = string.Empty;
    }
}

