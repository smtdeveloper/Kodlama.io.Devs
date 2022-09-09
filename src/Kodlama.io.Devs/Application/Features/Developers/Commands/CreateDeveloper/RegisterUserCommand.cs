using Application.Features.Developers.Dto;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.CreateDeveloper
{
    public class RegisterUserCommand : UserForRegisterDto, IRequest<TokenDto>
    {

        public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenDto>
        {
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;

            public CreateUserCommandHandler(IDeveloperRepository developerRepository, IMapper mapper, ITokenHelper tokenHelper)
            {
                _developerRepository = developerRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;

            }

            public async Task<TokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
               

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                Developer developer = _mapper.Map<Developer>(request);
                developer.PasswordHash = passwordHash;
                developer.PasswordSalt = passwordSalt;

                var createdDeveloper = await _developerRepository.AddAsync(developer);

                var token = _tokenHelper.CreateToken(developer, new List<OperationClaim>());

                return new() { Token = token.Token, Expiration = token.Expiration };
            }
        }

           

    }
}
