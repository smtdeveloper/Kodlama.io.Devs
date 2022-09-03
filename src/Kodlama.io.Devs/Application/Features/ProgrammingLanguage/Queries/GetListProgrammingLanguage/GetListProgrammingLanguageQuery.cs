using Application.Features.ProgrammingLanguage.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.ProgrammingLanguage.Queries.GetListProgrammingLanguage;

public class GetListProgrammingLanguageQuery  : IRequest<ProgrammingLanguageListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery,
            ProgrammingLanguageListModel>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
        }
        public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            var programmingLanguages = await _programmingLanguageRepository
                .GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            var mappedProgrammingLanguageModel = _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages);
            
            return mappedProgrammingLanguageModel;

        }
    }
}