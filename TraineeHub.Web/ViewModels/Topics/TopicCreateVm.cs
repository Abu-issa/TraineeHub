using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Web.ViewModels.Topics
{
    public class TopicCreateVm
    {
        [Required]
        [MaxLength(150)] 
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}
