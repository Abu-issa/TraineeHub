using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Api.DTOs.Topics
{
    public class CreateTopicDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }

}
