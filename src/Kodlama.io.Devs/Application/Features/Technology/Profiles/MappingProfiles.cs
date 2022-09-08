using Application.Features.Technology.Dto;
using Application.Features.Technology.Model;
using Application.Features.Technology.Queires.GetById;
using AutoMapper;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<Domain.Entities.Technology>, GetListTechnologyModel>().ReverseMap();

            CreateMap<Domain.Entities.Technology, GetListTechnologyDto>()
                .ForMember(t => t.ProgrammingLanguageName , opt => opt
                .MapFrom(p => p.ProgrammingLanguage.Name)).ReverseMap();


            CreateMap<Domain.Entities.Technology, GetByIdTechnologyQuery>().ReverseMap();

            CreateMap<Domain.Entities.Technology, GetByIdTechnologyDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt
                .MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();

        }
    }
}
