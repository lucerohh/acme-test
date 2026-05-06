using ACME_api.ACME.Domain.Model.Commands.TaskItems;
using ACME_api.ACME.Domain.Model.Queries.TaskItems;
using ACME_api.ACME.Domain.Services.TaskItems;
using ACME_api.ACME.Interfaces.Rest.Resources.Task;
using ACME_api.ACME.Interfaces.Rest.Resources.TaskItems;
using ACME_api.ACME.Interfaces.Rest.Transform.Task;
using ACME_api.ACME.Interfaces.Rest.Transform.TaskItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME_api.ACME.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/task-items")]
    [Authorize]
    [Tags("task-items")]
    public class TasksController(ITaskItemQueryService _taskItemQueryService,
        ITaskItemCommandService _taskItemCommandService) : ControllerBase
    {

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get user tasks",
            Description = "Returns all tasks belonging to the authenticated user",
            OperationId = "GetTasks"
        )]
        [SwaggerResponse(200, "Tasks retrieved successfully", typeof(IEnumerable<TaskItemResource>))]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult> GetTasks()
        {
            var tasks = await _taskItemQueryService.Handle(new GetTaskItemsByUserQuery());

            var resources = tasks.Select(TaskItemResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(resources);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Create a new task",
            Description = "Creates a task for the authenticated user",
            OperationId = "CreateTask"
        )]
        [SwaggerResponse(201, "Task created successfully", typeof(TaskItemResource))]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskItemResource resource)
        {
            try
            {
                var command = CreateTaskItemCommandFromResourceAssembler.ToCommandFromResource(resource);

                var task = await _taskItemCommandService.Handle(command);

                var result = TaskItemResourceFromEntityAssembler.ToResourceFromEntity(task);

                return CreatedAtAction(nameof(GetTasks), new { id = result.Id }, result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Update a task",
            Description = "Updates a task if it belongs to the authenticated user",
            OperationId = "UpdateTask"
        )]
        [SwaggerResponse(200, "Task updated successfully", typeof(TaskItemResource))]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] UpdateTaskItemResource resource)
        {
            try
            {
                var command = UpdateTaskItemCommandFromResourceAssembler.ToCommandFromResource(id, resource);

                var task = await _taskItemCommandService.Handle(command);

                var result = TaskItemResourceFromEntityAssembler.ToResourceFromEntity(task);

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
                [SwaggerOperation(
            Summary = "Delete a task",
            Description = "Deletes a task if it belongs to the authenticated user",
            OperationId = "DeleteTask"
        )]
        [SwaggerResponse(204, "Task deleted successfully")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Task not found")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskItemCommandService.Handle(new DeleteTaskItemCommand(id));

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(
    Summary = "Get task by id",
    Description = "Returns a specific task if it belongs to the authenticated user",
    OperationId = "GetTaskById"
)]
        [SwaggerResponse(200, "Task retrieved successfully", typeof(TaskItemResource))]
        [SwaggerResponse(404, "Task not found")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult> GetTaskById(int id)
        {
            try
            {
                var query = new GetTaskItemByIdQuery(id);

                var task = await _taskItemQueryService.Handle(query);

                var resource = TaskItemResourceFromEntityAssembler
                    .ToResourceFromEntity(task);

                return Ok(resource);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
