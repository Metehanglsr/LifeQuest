using LifeQuestAPI.Application.Features.Roles.Commands.AssignRoleToUser;
using LifeQuestAPI.Application.Features.Roles.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeQuestAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole(CreateRoleCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("assign-user")]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}