using Application.Features.ProgrammingLanguage.Dtos;
using FluentValidation;

namespace Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommandValidator : AbstractValidator<CreateProgrammingLanguageDto>
{
    public CreateProgrammingLanguageCommandValidator()
    {
        RuleFor(f => f.Name).NotNull();
        RuleFor(f => f.Name).NotEmpty();
    }
}