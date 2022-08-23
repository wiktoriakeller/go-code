﻿using AutoMapper;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Queries;
using GoCode.Application.Courses.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/courses")]
    public class CoursesController : BaseApiController
    {
        public CoursesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var response = await _medaitor.Send(query);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserCourses()
        {
            var query = new GetUserCoursesQuery();
            var response = await _medaitor.Send(query);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var command = _mapper.Map<CreateCourseCommand>(request);
            var response = await _medaitor.Send(command);

            if (!response.Succeeded)
            {
                return StatusCode((int)response.HttpStatusCode, response);
            }

            return Created($"api/v1/courses/{response.Data?.Id}", response);
        }

        [HttpPatch("signup/{id}")]
        public async Task<IActionResult> SignUpForACourse([FromRoute] int id)
        {
            var command = new SignUpForCourseCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            var command = new DeleteCourseCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
