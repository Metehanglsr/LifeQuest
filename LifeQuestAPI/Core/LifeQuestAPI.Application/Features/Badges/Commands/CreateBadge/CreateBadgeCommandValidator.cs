using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LifeQuestAPI.Application.Features.Badges.Commands.CreateBadge;

public class CreateBadgeCommandValidator : AbstractValidator<CreateBadgeCommandRequest>
{
    public CreateBadgeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Rozet adı boş olamaz.")
            .MaximumLength(100).WithMessage("Rozet adı 100 karakterden uzun olamaz.");

        RuleFor(x => x.RequiredLevel)
            .GreaterThan(0).WithMessage("Gereken seviye en az 1 olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Rozet açıklaması gereklidir.");
    }
}