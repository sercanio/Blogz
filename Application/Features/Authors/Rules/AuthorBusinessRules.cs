using Application.Features.Authors.Constants;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Authors.Rules;

public class AuthorBusinessRules : BaseBusinessRules
{
    private readonly IAuthorRepository _authorRepository;
    //private readonly ILocalizationService _localizationService;

    public AuthorBusinessRules(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
        //_localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        //string message = await _localizationService.GetLocalizedAsync(messageKey, AuthorsBusinessMessages.SectionName);
        throw new BusinessException(messageKey);
    }

    public async Task AuthorShouldExistWhenSelected(Author? author)
    {
        if (author == null)
            await throwBusinessException(AuthorsBusinessMessages.AuthorNotExists);
    }

    public async Task AuthorIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Author? author = await _authorRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AuthorShouldExistWhenSelected(author);
    }

}