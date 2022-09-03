using Application.Features.ProgrammingLanguage.Dtos;
using FluentValidation;

namespace Application.Features.ProgrammingLanguage.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommandValidator : AbstractValidator<UpdatedProgrammingLanguageDto>
{
    public UpdateProgrammingLanguageCommandValidator()
    {
        RuleFor(f => f.Name).NotEmpty();
        RuleFor(f => f.Name).MinimumLength(2);

    }
}