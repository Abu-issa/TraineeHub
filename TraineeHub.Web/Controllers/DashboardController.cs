using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TraineeHub.Domain.Enum;
using TraineeHub.Infrastructure.Persistence;
using TraineeHub.Web.ViewModels.Dashboard;

namespace TraineeHub.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly TraineeHubDbContext _context;
        private readonly IDistributedCache _cache;

        private const string DashboardCacheKey = "dashboard_data";

        public DashboardController(TraineeHubDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IActionResult Index()
        {
            var cachedData = _cache.GetString(DashboardCacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedVm = JsonSerializer.Deserialize<DashboardVm>(cachedData);

                if (cachedVm != null)
                {
                    cachedVm.FromCache = true;
                    return View(cachedVm);
                }
            }

            var vm = new DashboardVm
            {
                TraineesCount = _context.Trainees.Count(),
                TopicsCount = _context.Topics.Count(),
                AssignmentsCount = _context.Assignments.Count(),

                PendingSubmissionsCount = _context.Submissions.Count(s => s.Stauts == SubmissionStatus.Pending),
                SubmittedSubmissionsCount = _context.Submissions.Count(s => s.Stauts == SubmissionStatus.Submitted),
                ApprovedSubmissionsCount = _context.Submissions.Count(s => s.Stauts == SubmissionStatus.Approve),
                RejectedSubmissionsCount = _context.Submissions.Count(s => s.Stauts == SubmissionStatus.Rejected),

                FromCache = false
            };

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            };

            var serializedData = JsonSerializer.Serialize(vm);
            _cache.SetString(DashboardCacheKey, serializedData, cacheOptions);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RefreshCache()
        {
            _cache.Remove(DashboardCacheKey);
            return RedirectToAction(nameof(Index));
        }
    }
}