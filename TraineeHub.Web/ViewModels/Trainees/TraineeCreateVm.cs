using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Web.ViewModels.Trainees
{
    public class TraineeCreateVm
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(63)]
        public string Email { get; set; } = string.Empty;
    }
}
