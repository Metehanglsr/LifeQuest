using LifeQuestAPI.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LifeQuestAPI.Application.Features.User.Queries.GetLeaderboard;
using LifeQuestAPI.Application.Features.User.Queries.GetUserProfile;

namespace LifeQuestAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized("Kimlik doğrulanamadı.");

        var request = new GetUserProfileQueryRequest { UserId = Guid.Parse(userIdString) };
        var response = await _mediator.Send(request);

        return Ok(response);
    }
    [HttpGet("leaderboard")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLeaderboard()
    {
        var response = await _mediator.Send(new GetLeaderboardQueryRequest());
        return Ok(response);
    }
}