using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LifeQuestAPI.Application.Features.UserBadges.Queries.GetBadgeGallery;

namespace LifeQuestAPI.API.Controllers;

[Route("api/user-badges")]
[ApiController]
[Authorize]
public class UserBadgesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserBadgesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("gallery")]
    public async Task<IActionResult> GetBadgeGallery()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var request = new GetBadgeGalleryQueryRequest
        {
            UserId = Guid.Parse(userIdString)
        };

        var response = await _mediator.Send(request);
        return Ok(response);
    }
}