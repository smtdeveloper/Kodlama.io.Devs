using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguage.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

       

        public async Task ProgrammingLanguageCannotBeDuplicatedWhenInserted(string name)
        {
            var result = await _programmingLanguageRepository.GetListAsync(w => w.Name.Equals(name));

            if (result.Items.Any())
                throw new BusinessException("Programlama Dili mevcuttur.");
        }

        public async Task ProgrammingLanguageShouldBeExist(int id)
        {
            var result = await _programmingLanguageRepository.GetAsync(w => w.Id == id);
            if (result is null) throw new BusinessException($"id ile programlama dili yoktur {id}");
        }
    }
}
