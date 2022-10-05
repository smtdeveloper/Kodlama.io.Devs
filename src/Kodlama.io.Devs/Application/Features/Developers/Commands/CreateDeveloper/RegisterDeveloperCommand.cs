using Application.Features.Developers.Dto;
using Application.Features.Developers.Rules;
using Application.Services.AuthService;
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
    public class RegisterDeveloperCommand : UserForRegisterDto, IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<RegisterDeveloperCommand, RegisteredDto>
        {
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly DeveloperBusinessRules _developerBusinessRules;
            private readonly IAuthService _authService;

            public CreateUserCommandHandler(IDeveloperRepository developerRepository, IMapper mapper, ITokenHelper tokenHelper, DeveloperBusinessRules developerBusinessRules, IAuthService authService)
            {
                _developerRepository = developerRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _developerBusinessRules = developerBusinessRules;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
            {
                await _developerBusinessRules.EmailCanNotBeDuplicatedWhenInserted(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                Developer developer = _mapper.Map<Developer>(request.UserForRegisterDto);
                developer.PasswordHash = passwordHash;
                developer.PasswordSalt = passwordSalt;

                var createdDeveloper = await _developerRepository.AddAsync(developer);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdDeveloper);
                RefreshToken createdRefreshToken =
                    await _authService.CreateRefreshToken(createdDeveloper, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken
                };

                return registeredDto;

            }

            
        }

           

    }
}
