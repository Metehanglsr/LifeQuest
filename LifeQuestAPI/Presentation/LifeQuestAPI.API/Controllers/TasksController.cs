using LifeQuestAPI.Application.Features.Tasks.Commands.CompleteTask;
using LifeQuestAPI.Application.Features.Tasks.Commands.CreateBulkTasks;
using LifeQuestAPI.Application.Features.Tasks.Commands.CreateTask;
using LifeQuestAPI.Application.Features.Tasks.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeQuestAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllTasks()
    {
        var response = await _mediator.Send(new GetAllTasksQueryRequest());
        return Ok(response);
    }
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteTask(CompleteTaskCommandRequest request)
    {
        var response = await _mediator.Send(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpPost("create-task")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTask(CreateTaskCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("create-bulk-tasks")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateBulkTasks(CreateBulkTasksCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}