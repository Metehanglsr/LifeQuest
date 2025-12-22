using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Application.Common.Security;
using LifeQuestAPI.Application.Repositories;
using LifeQuestAPI.Domain.Entities.Identity;
using MediatR;

namespace LifeQuestAPI.Application.Features.Auth.Commands.Register;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    private readonly IAppUserWriteRepository _userWriteRepository;
    private readonly IAppUserReadRepository _userReadRepository;
    private readonly IAppRoleReadRepository _roleReadRepository;

    public RegisterUserCommandHandler(IAppUserWriteRepository userWriteRepository, IAppUserReadRepository userReadRepository, IAppRoleReadRepository roleReadRepository)
    {
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _userReadRepository.GetSingleAsync(u => u.Email == request.Email, tracking: false);
        if (existingUser != null)
            throw new Exception("Bu email adresi zaten kullanılıyor.");

        HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var newUser = new AppUser
        {
            Name = request.Name,
            Surname = request.Surname,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            TotalXP = 0,
            GeneralLevel = 1,
            CreatedAt = DateTime.UtcNow
        };

        var defaultRole = await _roleReadRepository.GetSingleAsync(r => r.Name == "User", tracking: false);

        if (defaultRole != null)
        {
            newUser.AppRoleId = defaultRole.Id;
        }
        else
        {
            newUser.AppRole = new AppRole { Name = "User" };
        }

        await _userWriteRepository.AddAsync(newUser);
        await _userWriteRepository.SaveAsync();

        return new RegisterUserCommandResponse
        {
            IsSuccess = true,
            Message = "Kullanıcı başarıyla oluşturuldu. Giriş yapabilirsiniz."
        };
    }
}
