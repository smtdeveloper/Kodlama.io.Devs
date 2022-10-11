using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaim.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
    {
        public UpdateOperationClaimCommandValidator()
        {
            RuleFor(r => r.Role).NotEmpty().MinimumLength(2);

        }
    }
}
