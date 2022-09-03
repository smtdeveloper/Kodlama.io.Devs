using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries
{
    public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
    {
        public PageRequest pageRequest { get; set; }

        public class GetListBrandHandler : IRequestHandler<GetListProgrammingLanguageQuery, ProgrammingLanguageListModel>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;

            public GetListBrandHandler(IProgrammingLanguageRepository programmingLanguage, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguage;
                _mapper = mapper;
            }

            public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Domain.Entities.ProgrammingLanguage> ProgrammingLanguage = await _programmingLanguageRepository.GetListAsync( p => p.IsActive == true,  index: request.pageRequest.Page, size: request.pageRequest.PageSize);

                ProgrammingLanguageListModel mapperProgrammingLanguageListModel = _mapper.Map<ProgrammingLanguageListModel>(ProgrammingLanguage);

                return mapperProgrammingLanguageListModel;
            }
        }

    }
}
