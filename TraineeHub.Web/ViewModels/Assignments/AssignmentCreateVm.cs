using System.ComponentModel.DataAnnotations;
using TraineeHub.Web.Validation;

namespace TraineeHub.Web.ViewModels.Assignments
{
    public class AssignmentCreateVm
    {
        public Guid TopicTd { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Difficulty { get; set; }

        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime DutDate { get; set; }
    }
}
