using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeHub.Domain.Entities;
using TraineeHub.Infrastructure.Persistence;
using TraineeHub.Web.ViewModels.Trainees;

namespace TraineeHub.Web.Controllers
{
    public class TraineesController : Controller
    {
        private readonly TraineeHubDbContext _context;

        public TraineesController(TraineeHubDbContext context)
        {
            _context = context;
        }

        // GET: Trainees
        public IActionResult Index()
        {
            var trainees = _context.Trainees
                .Include(t => t.Submissions)
                .Select(t => new TraineeListItemVm
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    CreatAt = t.CreatAt,
                    SubmissionCount = t.Submissions.Count
                })
                .ToList();

            return View(trainees);
        }

        // GET: Trainees/Details/{id}
        public IActionResult Details(Guid id)
        {
            var trainee = _context.Trainees
                .Include(t => t.Submissions)
                .ThenInclude(s => s.Assignment)
                .FirstOrDefault(t => t.Id == id);

            if (trainee == null)
            {
                return NotFound();
            }

            var vm = new TraineeDetailsVm
            {
                Id = trainee.Id,
                FullName = trainee.FullName,
                Email = trainee.Email,
                CreatAt = trainee.CreatAt,
                SubmissionCount = trainee.Submissions.Count
            };

            return View(vm);
        }

        // GET: Trainees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TraineeCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var trainee = new Trainee
            {
                FullName = vm.FullName,
                Email = vm.Email
            };

            _context.Trainees.Add(trainee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Trainees/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var trainee = _context.Trainees.Find(id);

            if (trainee == null)
            {
                return NotFound();
            }

            var vm = new TraineeEditVm
            {
                Id = trainee.Id,
                FullName = trainee.FullName,
                Email = trainee.Email
            };

            return View(vm);
        }

        // POST: Trainees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, TraineeEditVm vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var trainee = _context.Trainees.Find(id);

            if (trainee == null)
            {
                return NotFound();
            }

            trainee.FullName = vm.FullName;
            trainee.Email = vm.Email;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}