using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries;

public class GetListByUserIdUserOperationClaimQueryValidator : AbstractValidator<GetListByUserIdUserOperationClaimQuery>
{
    public GetListByUserIdUserOperationClaimQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

    }
}

