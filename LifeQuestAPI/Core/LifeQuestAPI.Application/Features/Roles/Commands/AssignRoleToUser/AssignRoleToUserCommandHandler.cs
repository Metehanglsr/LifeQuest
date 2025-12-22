using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities.Identity;
using MediatR;

namespace LifeQuestAPI.Application.Features.Roles.Commands.AssignRoleToUser;

public sealed class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
{
    private readonly IAppUserReadRepository _userReadRepository;
    private readonly IAppUserWriteRepository _userWriteRepository;
    private readonly IAppRoleReadRepository _roleReadRepository;

    public AssignRoleToUserCommandHandler(
        IAppUserReadRepository userReadRepository,
        IAppUserWriteRepository userWriteRepository,
        IAppRoleReadRepository roleReadRepository)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(request.UserId.ToString(), tracking: true);
        if (user == null) throw new Exception("Kullanıcı bulunamadı.");

        var role = await _roleReadRepository.GetByIdAsync(request.RoleId.ToString());
        if (role == null) throw new Exception("Belirtilen rol sistemde bulunamadı.");

        user.AppRoleId = request.RoleId;

        _userWriteRepository.Update(user);
        await _userWriteRepository.SaveAsync();

        return new AssignRoleToUserCommandResponse
        {
            IsSuccess = true,
            Message = $"'{user.UserName}' adlı kullanıcının rolü başarıyla '{role.Name}' olarak değiştirildi."
        };
    }
}