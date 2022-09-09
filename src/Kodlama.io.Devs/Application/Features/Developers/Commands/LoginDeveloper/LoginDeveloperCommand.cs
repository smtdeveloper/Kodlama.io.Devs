using Application.Features.Developers.Dto;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Developers.Commands.LoginDeveloper
{
    public class LoginDeveloperCommand : UserForLoginDto, IRequest<TokenDto>
    {
        public class LoginDeveloperHandler : IRequestHandler<LoginDeveloperCommand, TokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;

            public LoginDeveloperHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            public async Task<TokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(
                    u => u.Email.ToLower() == request.Email.ToLower(),
                    include: m => m.Include(c => c.UserOperationClaims).ThenInclude(x => x.OperationClaim));
                
                List<OperationClaim> operationClaims = new List<OperationClaim>() { };

                foreach (var userOperationClaim in user.UserOperationClaims)
                {
                    operationClaims.Add(userOperationClaim.OperationClaim);
                }
                
                AccessToken token = _tokenHelper.CreateToken(user, operationClaims);

                TokenDto tokenDto = _mapper.Map<TokenDto>(token);

                return tokenDto;
            }
        }
    }
}
