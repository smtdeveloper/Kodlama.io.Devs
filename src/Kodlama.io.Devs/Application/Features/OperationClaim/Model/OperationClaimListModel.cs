using Application.Features.OperationClaim.Dto;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaim.Model
{
    public class OperationClaimListModel : BasePageableModel
    {
        public OperationClaimListDto[] Items { get; set; }
    }
}
