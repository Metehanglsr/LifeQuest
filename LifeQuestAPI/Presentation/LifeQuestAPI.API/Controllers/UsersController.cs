using LifeQuestAPI.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LifeQuestAPI.Application.Features.User.Queries.GetLeaderboard;
using LifeQuestAPI.Application.Features.User.Queries.GetUserProfile;
using LifeQuestAPI.Application.Features.User.Queries.GetUserStats;
using LifeQuestAPI.Application.Features.User.Queries.GetUserActivities;

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
            return Unauthorized();

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

    [HttpGet("activities")]
    public async Task<IActionResult> GetActivities()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized();

        var request = new GetUserActivitiesQueryRequest { UserId = Guid.Parse(userIdString) };
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized();

        var request = new GetUserStatsQueryRequest { UserId = Guid.Parse(userIdString) };
        var response = await _mediator.Send(request);

        return Ok(response);
    }
}