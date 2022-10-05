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
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IAuthService _authService;
            private readonly DeveloperBusinessRules _developerBusinessRules;

            public CreateUserCommandHandler(IDeveloperRepository developerRepository, IUserRepository userRepository, ITokenHelper tokenHelper, IAuthService authService, DeveloperBusinessRules developerBusinessRules)
            {
                _developerRepository = developerRepository;
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _authService = authService;
                _developerBusinessRules = developerBusinessRules;
            }

            public async Task<RegisteredDto> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
            {
                await _developerBusinessRules.EmailCanNotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                Developer newUser = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true
                    
                };

                Developer createdUser = await _developerRepository.AddAsync(newUser);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,
                };
                return registeredDto;

            }

            
        }

           

    }
}
