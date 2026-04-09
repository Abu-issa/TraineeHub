using Microsoft.AspNetCore.Mvc;
using TraineeHub.Api.DTOs.Topics;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;

namespace TraineeHub.Api.Controllers
{
    /// <summary>
    /// Manage topics in the system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicsController"/> class.
        /// </summary>
        /// <param name="topicRepository">Repository used to manage topics.</param>
        public TopicsController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        /// <summary>
        /// Get all topics.
        /// </summary>
        /// <returns>A list of all topics.</returns>
        /// <response code="200">Returns the list of topics.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var topics = _topicRepository.GetAll()
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description
                })
                .ToList();

            return Ok(topics);
        }

        /// <summary>
        /// Get a topic by ID.
        /// </summary>
        /// <param name="id">The topic ID.</param>
        /// <returns>The topic details if found.</returns>
        /// <response code="200">Returns the topic details.</response>
        /// <response code="404">If the topic is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var topic = _topicRepository.GetById(id);

            if (topic == null)
                return NotFound();

            var result = new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description
            };

            return Ok(result);
        }

        /// <summary>
        /// Create a new topic.
        /// </summary>
        /// <param name="dto">The topic data.</param>
        /// <returns>The created topic.</returns>
        /// <response code="201">Returns the newly created topic.</response>
        /// <response code="400">If the request data is invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateTopicDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var topic = new Topic
            {
                Title = dto.Title,
                Description = dto.Description
            };

            _topicRepository.Add(topic);

            var result = new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = topic.Id }, result);
        }
    }
}