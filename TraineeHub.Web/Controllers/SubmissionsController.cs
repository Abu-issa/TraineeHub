using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TraineeHub.Application.Interfaces;
using TraineeHub.Messaging.Common.Events;
using TraineeHub.Messaging.Common.Interfaces;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Enum;
using TraineeHub.Infrastructure.Persistence;
using TraineeHub.Web.ViewModels.Submissions;

namespace TraineeHub.Web.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly TraineeHubDbContext _context;
        private readonly IMessagePublisher _publisher;
        private readonly IExportService _excelExporter;

        public SubmissionsController(TraineeHubDbContext context, IMessagePublisher publisher, IExportService exportService)
        {
            _context = context;
            _publisher = publisher;
            _excelExporter = exportService;
        }

        // GET: Submissions
        public IActionResult Index(string? status)
        {
            var submissionsQuery = _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Trainee)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<SubmissionStatus>(status, out var parsedStatus))
            {
                submissionsQuery = submissionsQuery.Where(s => s.Stauts == parsedStatus);
            }

            var submissions = submissionsQuery
                .Select(s => new SubmissionListItemVm
                {
                    Id = s.Id,
                    AssignmentId = s.AssignmentId,
                    AssignmentTitle = s.Assignment.Title,
                    TraineerId = s.TraineerId,
                    TraineeFullName = s.Trainee.FullName,
                    Notes = s.Notes,
                    Stauts = s.Stauts,
                    SubmittedAt = s.SubmittedAt
                })
                .ToList();

            ViewBag.SelectedStatus = status;

            return View(submissions);
        }

        // GET: Submissions/Create
        public IActionResult Create()
        {
            var vm = new SubmissionCreateVm
            {
                Trainees = _context.Trainees
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.FullName
                    })
                    .ToList(),

                Assignments = _context.Assignments
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Title
                    })
                    .ToList()
            };

            return View(vm);
        }

        // POST: Submissions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubmissionCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Trainees = _context.Trainees
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.FullName
                    })
                    .ToList();

                vm.Assignments = _context.Assignments
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Title
                    })
                    .ToList();

                return View(vm);
            }

            var submission = new Submission
            {
                AssignmentId = vm.AssignmentId,
                TraineerId = vm.TraineerId,
                Notes = vm.Notes,
                Stauts = SubmissionStatus.Submitted,
                SubmittedAt = DateTime.UtcNow
            };

            _context.Submissions.Add(submission);
            _context.SaveChanges();

            _publisher.Publish(new SubmissionCreatedEvent
            {
                SubmissionId = submission.Id,
                TraineeName = _context.Trainees.First(t => t.Id == submission.TraineerId).FullName,
                Topic = _context.Assignments.First(a => a.Id == submission.AssignmentId).Title
            });


            return RedirectToAction(nameof(Index));
        }

        // POST: Submissions/Approve/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(Guid id)
        {
            var submission = _context.Submissions.FirstOrDefault(s => s.Id == id);

            if (submission == null)
            {
                return NotFound();
            }

            submission.Stauts = SubmissionStatus.Approve;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: Submissions/Reject/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(Guid id)
        {
            var submission = _context.Submissions.FirstOrDefault(s => s.Id == id);

            if (submission == null)
            {
                return NotFound();
            }

            submission.Stauts = SubmissionStatus.Rejected;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExportExcel()
        {
            var data = _context.Submissions
                .Include(t => t.Trainee)
                .Include(t => t.Assignment)
                .Select(t => new SubmissionExportVm
                {
                    TraineeFullName = t.Trainee.FullName,

                    AssignmentTitle = t.Assignment.Title,

                    Notes = t.Notes,
                    Stauts = t.Stauts,
                    SubmittedAt = t.SubmittedAt,


                    ReviewAction =
    t.Stauts == SubmissionStatus.Submitted ? "Needs Review" :
    t.Stauts == SubmissionStatus.Pending ? "Waiting Review" :
    "Already Reviewed"



                }).ToList();
            var file = _excelExporter.ExportToExcel(data, "Submissions");
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Submissions.xlsx");
        }

        public IActionResult ExportCsv()
        {
            var data = _context.Submissions
               .Include(t => t.Trainee)
               .Include(t => t.Assignment)
               .Select(t => new SubmissionExportVm
               {
                   TraineeFullName = t.Trainee.FullName,

                   AssignmentTitle = t.Assignment.Title,

                   Notes = t.Notes,
                   Stauts = t.Stauts,
                   SubmittedAt = t.SubmittedAt,


                   ReviewAction =
   t.Stauts == SubmissionStatus.Submitted ? "Needs Review" :
   t.Stauts == SubmissionStatus.Pending ? "Waiting Review" :
   "Already Reviewed"



               }).ToList();
            var file = _excelExporter.ExportToExcel(data, "Submissions");
            return File(file, "text/csv", "Submissions.csv");
        }

    }
}