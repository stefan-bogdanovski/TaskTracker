using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Management;
using Business.Management.ProjectOperations;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Business.DTO;
using Business.Validation;

namespace TaskTracker.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly TaskTrackerContext _context;

		public ProjectsController(TaskTrackerContext context)
		{
			_context = context;
		}
		// GET: api/<ProjectsController>
		[HttpGet]
		public IActionResult Get([FromQuery] ProjectFilterDto filterDto)
		{
			try
			{
				var result = OperationManager.GetManager.ExecuteOperation(new GetAllProjectsOperaton(this._context, filterDto));
				return Ok(result.Data);
			}
			catch(Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		
		}

		
		// GET api/<ProjectsController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			try
			{
				var result = OperationManager.GetManager.ExecuteOperation(new GetOneProject(this._context, id));
				if (!result.IsSuccessful)
				{
					return StatusCode(StatusCodes.Status404NotFound);
				}
				return Ok(result.Data);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		[HttpGet("{projectId}/Tasks")]
		public IActionResult GetAllProjectTasks(int projectId)
		{
			try
			{
				var opResult = OperationManager.GetManager.ExecuteOperation(new GetAllTasksForOneProject(_context, projectId));
				if (!opResult.IsSuccessful)
				{
					var errors = opResult.Errors.Select(err => new
					{
						ErrorMessage = err
					});
					return BadRequest(errors);
				}
				return Ok(opResult.Data);
			}
			catch(Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		// POST api/<ProjectsController>
		[HttpPost]
		public IActionResult Post([FromBody] ProjectDto dto)
		{
			try
			{
				var validator = new CreateProjectValidator(this._context);
				var result = validator.Validate(dto);

				if (!result.IsValid)
				{
					var errors = result.Errors.Select(err => new
					{
						PropertyName = err.PropertyName,
						PropertyError = err.ErrorMessage
					});
					return BadRequest(errors);
				}

				var OpResult = OperationManager
					.GetManager
					.ExecuteOperation(new AddProjectOperation(_context, dto));

				if (!OpResult.IsSuccessful)
				{
					var errors = OpResult.Errors.Select(err => new
					{
						ErrorMessage = err
					});
					return BadRequest(errors);
				}
				return StatusCode(StatusCodes.Status201Created);
			}
			catch(Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			
		}

		// PUT api/<ProjectsController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] ProjectDto dto)
		{
			try
			{
				var validator = new EditProductValidator(_context, id);
				var validatorResult = validator.Validate(dto);
				if (!validatorResult.IsValid)
				{
					var errors = validatorResult.Errors.Select(err => new
					{
						PropertyName = err.PropertyName,
						PropertyError = err.ErrorMessage
					});
					return BadRequest(errors);
				}
				var opResult = OperationManager.GetManager.ExecuteOperation(new EditProjectOperation(_context, id, dto));
				if (!opResult.IsSuccessful)
				{
					var errors = opResult.Errors.Select(err => new
					{
						ErrorMessage = err
					});
					return BadRequest(errors);
				}
				return NoContent();
			}
			catch(Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// DELETE api/<ProjectsController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var result = OperationManager.GetManager.ExecuteOperation(new DeleteProjectOperation(_context, id));
				if(!result.IsSuccessful)
				{
					var errors = result.Errors.Select(err => new
					{
						ErrorMessage = err
					});
					return BadRequest(errors);
				}
				return NoContent();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
