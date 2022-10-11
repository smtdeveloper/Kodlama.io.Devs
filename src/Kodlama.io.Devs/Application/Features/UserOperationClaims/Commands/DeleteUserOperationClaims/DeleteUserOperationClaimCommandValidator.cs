using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaims;
public class DeleteUserOperationClaimCommandValidator : AbstractValidator<DeleteUserOperationClaimCommand>
{
    public DeleteUserOperationClaimCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();

    }
}
