using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeHub.Domain.Entities;
using TraineeHub.Infrastructure.Persistence;
using TraineeHub.Web.ViewModels.Assignments;
using TraineeHub.Web.ViewModels.Topics;

namespace TraineeHub.Web.Controllers
{
    public class TopicsController : Controller
    {
        private readonly TraineeHubDbContext _context;

        public TopicsController(TraineeHubDbContext context)
        {
            _context = context;
        }

        // GET: Topics
        public IActionResult Index()
        {
            var topics = _context.Topics
                .Include(t => t.Assignments)
                .Select(t => new TopicListItemVm
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    AssignmentCount = t.Assignments.Count
                })
                .ToList();

            return View(topics);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TopicCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var topic = new Topic
            {
                Title = vm.Title,
                Description = vm.Description
            };

            _context.Topics.Add(topic);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Topics/Details/{id}
        public IActionResult Details(Guid id)
        {
            var topic = _context.Topics
                .Include(t => t.Assignments)
                .FirstOrDefault(t => t.Id == id);

            if (topic == null)
            {
                return NotFound();
            }

            var vm = new TopicDetailsVm
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                Assignments = topic.Assignments.Select(a => new TopicAssignmentItemVm
                {
                    Id = a.Id,
                    Title = a.Title,
                    Difficulty = a.Difficulty,
                    DueDate = a.DutDate
                }).ToList(),
                NewAssignment = new AssignmentCreateVm
                {
                    TopicTd = topic.Id
                }
            };

            return View(vm);
        }

        // POST: Topics/AddAssignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAssignment(AssignmentCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                var topic = _context.Topics
                    .Include(t => t.Assignments)
                    .FirstOrDefault(t => t.Id == vm.TopicTd);

                if (topic == null)
                {
                    return NotFound();
                }

                var detailsVm = new TopicDetailsVm
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    Description = topic.Description,
                    Assignments = topic.Assignments.Select(a => new TopicAssignmentItemVm
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Difficulty = a.Difficulty,
                        DueDate = a.DutDate
                    }).ToList(),
                    NewAssignment = vm
                };

                return View("Details", detailsVm);
            }

            var assignment = new Assignment
            {
                TopicTd = vm.TopicTd,
                Title = vm.Title,
                Description = vm.Description,
                Difficulty = vm.Difficulty,
                DutDate = vm.DutDate
            };

            _context.Assignments.Add(assignment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = vm.TopicTd });
        }
    }
}