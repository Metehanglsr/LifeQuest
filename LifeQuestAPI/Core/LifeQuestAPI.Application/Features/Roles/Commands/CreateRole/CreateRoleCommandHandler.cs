using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities.Identity;
using MediatR;

namespace LifeQuestAPI.Application.Features.Roles.Commands.CreateRole;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    private readonly IAppRoleWriteRepository _roleWriteRepository;

    public CreateRoleCommandHandler(IAppRoleWriteRepository roleWriteRepository)
    {
        _roleWriteRepository = roleWriteRepository;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var newRole = new AppRole
        {
            Id = Guid.NewGuid(),
            Name = request.RoleName,
        };

        await _roleWriteRepository.AddAsync(newRole);
        await _roleWriteRepository.SaveAsync();

        return new CreateRoleCommandResponse
        {
            IsSuccess = true,
            Message = $"'{newRole.Name}' rolü başarıyla oluşturuldu.",
            RoleId = newRole.Id
        };
    }
}
