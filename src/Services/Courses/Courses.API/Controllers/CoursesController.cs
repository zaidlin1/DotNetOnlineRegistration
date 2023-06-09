﻿using Courses.API.Entities;
using Courses.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Courses.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IcourseRepository _repository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(IcourseRepository repository, ILogger<CoursesController> logger
            )
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Course>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _repository.GetCourses();
            return Ok(courses);
        }

        [HttpGet("{id:length(24)}", Name = "GetCourse")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> GetCourseById(string id)
        {
            var course = await _repository.GetCourse(id);
            if (course == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(course);
        }


        [HttpPost("CheckCourses")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CheckCourses([FromBody] IEnumerable<Course> courses)
        {
            var isCompleted = await _repository.CheckCourses(courses);

            return Ok(isCompleted);
        }

        [HttpPost("CheckIntersectCourses")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CheckIntersectCourses([FromBody] IEnumerable<Course> courses)
        {
            var getMessage = await _repository.CheckIntersectCourses(courses);

            return Ok(getMessage);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        {
            await _repository.CreateCourse(course);

            return CreatedAtRoute("GetCourse", new { id = course.Id }, course);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCourse([FromBody] Course course)
        {
            return Ok(await _repository.UpdateCourse(course));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteCourse")]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCourseById(string id)
        {
            return Ok(await _repository.DeleteCourse(id));
        }
    }
}
