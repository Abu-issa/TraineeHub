using Microsoft.AspNetCore.Mvc;
using TraineeHub.Api.DTOs.Submissions;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Enum;
using TraineeHub.Domain.Intreface;

namespace TraineeHub.Api.Controllers
{
    /// <summary>
    /// Manage submissions in the system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionsRepository _submissionsRepository;
        private readonly ITraineeRepositroy _traineeRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmissionsController"/> class.
        /// </summary>
        /// <param name="submissionsRepository">Repository used to manage submissions.</param>
        /// <param name="traineeRepository">Repository used to manage trainees.</param>
        /// <param name="assignmentRepository">Repository used to manage assignments.</param>
        public SubmissionsController(
            ISubmissionsRepository submissionsRepository,
            ITraineeRepositroy traineeRepository,
            IAssignmentRepository assignmentRepository)
        {
            _submissionsRepository = submissionsRepository;
            _traineeRepository = traineeRepository;
            _assignmentRepository = assignmentRepository;
        }

        /// <summary>
        /// Get all submissions with optional filters and paging.
        /// </summary>
        /// <param name="status">Optional filter by submission status.</param>
        /// <param name="traineeId">Optional filter by trainee ID.</param>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>A paged list of submissions.</returns>
        /// <response code="200">Returns the paged list of submissions.</response>
        /// <response code="400">If page or pageSize is invalid.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll(
            [FromQuery] SubmissionStatus? status = null,
            [FromQuery] Guid? traineeId = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "page and pageSize must be greater than 0" });

            var submissions = _submissionsRepository.GetAll();

            // Filter by status
            if (status.HasValue)
            {
                submissions = submissions.Where(s => s.Stauts == status.Value).ToList();
            }

            // Filter by traineeId
            if (traineeId.HasValue)
            {
                submissions = submissions.Where(s => s.TraineerId == traineeId.Value).ToList();
            }

            var totalItems = submissions.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = submissions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SubmissionDto
                {
                    Id = s.Id,
                    AssignmentId = s.AssignmentId,
                    TraineerId = s.TraineerId,
                    Notes = s.Notes,
                    Stauts = s.Stauts.ToString(),
                    SubmittedAt = s.SubmittedAt
                })
                .ToList();

            var response = new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        /// <summary>
        /// Get a submission by ID.
        /// </summary>
        /// <param name="id">The submission ID.</param>
        /// <returns>The submission details if found.</returns>
        /// <response code="200">Returns the submission details.</response>
        /// <response code="404">If the submission is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var submission = _submissionsRepository.GetById(id);

            if (submission == null)
                return NotFound();

            var result = new SubmissionDto
            {
                Id = submission.Id,
                AssignmentId = submission.AssignmentId,
                TraineerId = submission.TraineerId,
                Notes = submission.Notes,
                Stauts = submission.Stauts.ToString(),
                SubmittedAt = submission.SubmittedAt
            };

            return Ok(result);
        }

        /// <summary>
        /// Create a new submission.
        /// </summary>
        /// <param name="dto">The submission data.</param>
        /// <returns>The created submission.</returns>
        /// <response code="201">Returns the newly created submission.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="404">If the trainee or assignment is not found.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] CreateSubmissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainee = _traineeRepository.GetById(dto.TraineerId);
            if (trainee == null)
                return NotFound(new { message = "Trainee not found" });

            var assignment = _assignmentRepository.GetById(dto.AssignmentId);
            if (assignment == null)
                return NotFound(new { message = "Assignment not found" });

            var submission = new Submission
            {
                TraineerId = dto.TraineerId,
                AssignmentId = dto.AssignmentId,
                Notes = dto.Notes
            };

            _submissionsRepository.Add(submission);

            var result = new SubmissionDto
            {
                Id = submission.Id,
                AssignmentId = submission.AssignmentId,
                TraineerId = submission.TraineerId,
                Notes = submission.Notes,
                Stauts = submission.Stauts.ToString(),
                SubmittedAt = submission.SubmittedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = submission.Id }, result);
        }

        /// <summary>
        /// Approve a submission.
        /// </summary>
        /// <param name="id">The submission ID.</param>
        /// <returns>No content if approved successfully.</returns>
        /// <response code="204">Submission approved successfully.</response>
        /// <response code="404">If the submission is not found.</response>
        [HttpPost("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Approve(Guid id)
        {
            var submission = _submissionsRepository.GetById(id);
            if (submission == null)
                return NotFound();

            submission.Stauts = SubmissionStatus.Approve;
            _submissionsRepository.Update(submission);

            return NoContent();
        }

        /// <summary>
        /// Reject a submission.
        /// </summary>
        /// <param name="id">The submission ID.</param>
        /// <returns>No content if rejected successfully.</returns>
        /// <response code="204">Submission rejected successfully.</response>
        /// <response code="404">If the submission is not found.</response>
        [HttpPost("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Reject(Guid id)
        {
            var submission = _submissionsRepository.GetById(id);
            if (submission == null)
                return NotFound();

            submission.Stauts = SubmissionStatus.Rejected;
            _submissionsRepository.Update(submission);

            return NoContent();
        }
    }
}