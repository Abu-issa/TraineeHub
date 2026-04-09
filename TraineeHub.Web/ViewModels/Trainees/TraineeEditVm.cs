using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Web.ViewModels.Trainees
{
    public class TraineeEditVm
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
    }
}
