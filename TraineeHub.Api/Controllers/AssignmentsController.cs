using Microsoft.AspNetCore.Mvc;
using TraineeHub.Api.DTOs.Assignments;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;

namespace TraineeHub.Api.Controllers
{
    /// <summary>
    /// Manage assignments under a specific topic.
    /// </summary>
    [Route("api/topics/{topicId}/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentsController"/> class.
        /// </summary>
        /// <param name="topicRepository">Repository used to manage topics.</param>
        /// <param name="assignmentRepository">Repository used to manage assignments.</param>
        public AssignmentsController(ITopicRepository topicRepository, IAssignmentRepository assignmentRepository)
        {
            _topicRepository = topicRepository;
            _assignmentRepository = assignmentRepository;
        }

        /// <summary>
        /// Get all assignments for a specific topic.
        /// </summary>
        /// <param name="topicId">The topic ID.</param>
        /// <returns>A list of assignments for the topic.</returns>
        /// <response code="200">Returns the list of assignments.</response>
        /// <response code="404">If the topic is not found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll(Guid topicId)
        {
            var topic = _topicRepository.GetById(topicId);
            if (topic == null)
                return NotFound(new { message = "Topic not found" });

            var assignments = _assignmentRepository.GetByTopicId(topicId)
                .Select(a => new AssignmentDto
                {
                    Id = a.Id,
                    TopicTd = a.TopicTd,
                    Title = a.Title,
                    Description = a.Description,
                    Difficulty = a.Difficulty,
                    DutDate = a.DutDate
                })
                .ToList();

            return Ok(assignments);
        }

        /// <summary>
        /// Get a specific assignment by ID under a topic.
        /// </summary>
        /// <param name="topicId">The topic ID.</param>
        /// <param name="id">The assignment ID.</param>
        /// <returns>The assignment details if found.</returns>
        /// <response code="200">Returns the assignment details.</response>
        /// <response code="404">If the topic or assignment is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid topicId, Guid id)
        {
            var topic = _topicRepository.GetById(topicId);
            if (topic == null)
                return NotFound(new { message = "Topic not found" });

            var assignment = _assignmentRepository.GetById(id);
            if (assignment == null || assignment.TopicTd != topicId)
                return NotFound(new { message = "Assignment not found" });

            var result = new AssignmentDto
            {
                Id = assignment.Id,
                TopicTd = assignment.TopicTd,
                Title = assignment.Title,
                Description = assignment.Description,
                Difficulty = assignment.Difficulty,
                DutDate = assignment.DutDate
            };

            return Ok(result);
        }

        /// <summary>
        /// Create a new assignment under a specific topic.
        /// </summary>
        /// <param name="topicId">The topic ID.</param>
        /// <param name="dto">The assignment data.</param>
        /// <returns>The created assignment.</returns>
        /// <response code="201">Returns the newly created assignment.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="404">If the topic is not found.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create(Guid topicId, [FromBody] CreateAssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var topic = _topicRepository.GetById(topicId);
            if (topic == null)
                return NotFound(new { message = "Topic not found" });

            var assignment = new Assignment
            {
                TopicTd = topicId,
                Title = dto.Title,
                Description = dto.Description,
                Difficulty = dto.Difficulty,
                DutDate = dto.DutDate
            };

            _assignmentRepository.Add(assignment);

            var result = new AssignmentDto
            {
                Id = assignment.Id,
                TopicTd = assignment.TopicTd,
                Title = assignment.Title,
                Description = assignment.Description,
                Difficulty = assignment.Difficulty,
                DutDate = assignment.DutDate
            };

            return CreatedAtAction(nameof(GetById), new { topicId = topicId, id = assignment.Id }, result);
        }
    }
}