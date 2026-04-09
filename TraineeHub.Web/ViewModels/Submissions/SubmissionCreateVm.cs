using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Web.ViewModels.Submissions
{
    public class SubmissionCreateVm
    {
        [Required]
        public Guid AssignmentId { get; set; }

        [Required]
        public Guid TraineerId { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;
        public List<SelectListItem> Trainees { get; set; } = new();
        public List<SelectListItem> Assignments { get; set; } = new();
    }
}
