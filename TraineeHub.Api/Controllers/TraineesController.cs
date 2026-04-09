using Microsoft.AspNetCore.Mvc;
using TraineeHub.Api.DTOs.Trainees;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;

namespace TraineeHub.Api.Controllers
{
    /// <summary>
    /// Manage trainees in the system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TraineesController : ControllerBase
    {
        private readonly ITraineeRepositroy _traineeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraineesController"/> class.
        /// </summary>
        /// <param name="traineeRepository">Repository used to manage trainees.</param>
        public TraineesController(ITraineeRepositroy traineeRepository)
        {
            _traineeRepository = traineeRepository;
        }

        /// <summary>
        /// Get all trainees with paging and optional search.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <param name="search">Optional search by full name or email.</param>
        /// <returns>A paged list of trainees.</returns>
        /// <response code="200">Returns the paged list of trainees.</response>
        /// <response code="400">If page or pageSize is invalid.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "page and pageSize must be greater than 0" });

            var trainees = _traineeRepository.GetAll();

            // Filter by search
            if (!string.IsNullOrWhiteSpace(search))
            {
                trainees = trainees
                    .Where(t => t.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)
                             || t.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Paging
            var totalItems = trainees.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = trainees
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TraineeDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    CreatAt = t.CreatAt
                })
                .ToList();

            // Return paged response
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
        /// Get a trainee by ID.
        /// </summary>
        /// <param name="id">The trainee ID.</param>
        /// <returns>The trainee details if found.</returns>
        /// <response code="200">Returns the trainee details.</response>
        /// <response code="404">If the trainee is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var trainee = _traineeRepository.GetById(id);

            if (trainee == null)
                return NotFound();

            var result = new TraineeDto
            {
                Id = trainee.Id,
                FullName = trainee.FullName,
                Email = trainee.Email,
                CreatAt = trainee.CreatAt
            };

            return Ok(result);
        }

        /// <summary>
        /// Create a new trainee.
        /// </summary>
        /// <param name="dto">The trainee data.</param>
        /// <returns>The created trainee.</returns>
        /// <response code="201">Returns the newly created trainee.</response>
        /// <response code="400">If the request data is invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateTraineeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainee = new Trainee
            {
                FullName = dto.FullName,
                Email = dto.Email
            };

            _traineeRepository.Add(trainee);

            var result = new TraineeDto
            {
                Id = trainee.Id,
                FullName = trainee.FullName,
                Email = trainee.Email,
                CreatAt = trainee.CreatAt
            };

            return CreatedAtAction(nameof(GetById), new { id = trainee.Id }, result);
        }
    }
}