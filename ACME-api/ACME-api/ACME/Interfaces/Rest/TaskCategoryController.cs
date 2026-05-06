using ACME_api.ACME.Application.Internal.QueryServices;
using ACME_api.ACME.Domain.Model.Commands.TaskCategories;
using ACME_api.ACME.Domain.Model.Queries.TaskCategories;
using ACME_api.ACME.Domain.Services.TaskCategories;
using ACME_api.ACME.Interfaces.Rest.Resources.TaskCategories;
using ACME_api.ACME.Interfaces.Rest.Transform.TaskCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME_api.ACME.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/task-categories")]
    [Authorize]
    [Tags("task-categories")]
    public class TaskCategoryController(ITaskCategoryCommandService _taskCategoryCommandService,
        ITaskCategoryQueryService _taskCategoryQueryService) : ControllerBase
    {

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Create a task category",
            Description = "Creates a category for the authenticated user",
            OperationId = "CreateTaskCategory"
        )]
        [SwaggerResponse(201, "Category created successfully")]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<ActionResult> Create([FromBody] CreateTaskCategoryResource resource)
        {
            try
            {
                var command = CreateTaskCategoryCommandFromResourceAssembler
                    .ToCommandFromResource(resource);

                var category = await _taskCategoryCommandService.Handle(command);

                return Created(string.Empty, new
                {
                    category.Id,
                    category.Name
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get task categories of authenticated user",
            OperationId = "GetTaskCategories"
        )]
        [SwaggerResponse(200, "Categories retrieved successfully")]
        public async Task<ActionResult<IEnumerable<TaskCategoryResource>>> GetAll()
        {
            var categories = await _taskCategoryQueryService.Handle(new GetAllTaskCategoriesByUserQuery());

            var resources = categories.Select(TaskCategoryResourceFromEntityAssembler
                .ToResourceFromEntity);

            return Ok(resources);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(
    Summary = "Delete a task category",
    Description = "Deletes a task category if it belongs to the authenticated user",
    OperationId = "DeleteTaskCategory"
)]
        [SwaggerResponse(204, "Category deleted successfully")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Category not found")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult> DeleteTaskCategory(int id)
        {
            try
            {
                await _taskCategoryCommandService.Handle(
                    new DeleteTaskCategoryCommand(id)
                );

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
    }
}
