using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;

namespace Application.Features.ProgrammingLanguage.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }
    
    public void ProgrammingLanguageNameCannotBeNull(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new BusinessException("Programming Language cannot be null");
    }
    
    public async Task ProgrammingLanguageCannotBeDuplicatedWhenInserted(string name)
    {
        var result = await _programmingLanguageRepository.GetListAsync(w => w.Name.Equals(name));

        if (result.Items.Any())
            throw new BusinessException("Programming Language exist.");
    }
    
    public async Task ProgrammingLanguageNoChangeWhileUpdating(string name, int id)
    {
        var result = await _programmingLanguageRepository.GetAsync(w => w.Name.Equals(name) && 
                                                                        w.Id == id);

        if (result is not null)
            throw new BusinessException("There is no change");
    }

    public async Task ProgrammingLanguageShouldBeExist(int id)
    {
        var result = await _programmingLanguageRepository.GetAsync(w => w.Id == id);
        if (result is null) throw new BusinessException($"There is no programming language with id {id}");
    }
}
