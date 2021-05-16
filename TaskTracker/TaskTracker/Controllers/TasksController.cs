using Business.DTO;
using Business.Management;
using Business.Management.TaskOpertaions;
using Business.Validation;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TaskTracker.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly TaskTrackerContext _context;
		public TasksController(TaskTrackerContext context)
		{
			_context = context;
		}
		// GET: api/<TasksController>
		[HttpGet]
		public IActionResult Get([FromQuery] TaskFilterDto taskFilterDto)
		{
			try
			{
				var OpResult = OperationManager.GetManager.ExecuteOperation(new GetAllTasksOperation(_context, taskFilterDto));
				if (!OpResult.IsSuccessful)
				{
					var errors = OpResult.Errors.Select(e => new
					{
						ErrorMessage = e
					});
					return BadRequest(errors);
				}
				return Ok(OpResult.Data);
			}
			catch(Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		// GET api/<TasksController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			try
			{
				var result = OperationManager.GetManager.ExecuteOperation(new GetOneTaskOperation(this._context, id));
				if (!result.IsSuccessful)
				{
					var errors = result.Errors.Select(e => new
					{
						ErrorMessage = e
					});
					return NotFound(errors);
				}
				return Ok(result.Data);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// POST api/<TasksController>
		[HttpPost]
		public IActionResult Post([FromBody] TaskDto dto)
		{
			var validator = new CreateTaskValidator(_context);
			var result = validator.Validate(dto);
			if(!result.IsValid)
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
					.ExecuteOperation(new AddTaskOperation(_context, dto));

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

		// PUT api/<TasksController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] TaskDto dto)
		{
			try
			{
				var validator = new EditTaskValidator(_context, id);
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
				var opResult = OperationManager.GetManager.ExecuteOperation(new EditTaskOperation(_context, dto, id));
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
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// DELETE api/<TasksController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var result = OperationManager.GetManager.ExecuteOperation(new DeleteTaskOperation(_context, id));
				if (!result.IsSuccessful)
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
