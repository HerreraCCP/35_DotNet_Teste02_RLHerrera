using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class RegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly EmailService _emailService;

        public RegisterService(IMapper mapper, UserManager<CustomIdentityUser> userManager, EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result UserRegistration(CreateUserDto createDto)
        {
            var user = _mapper.Map<User>(createDto);
            var userIdentity = _mapper.Map<CustomIdentityUser>(user);
            var resultIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);

            
            ///Only for test different user's roles
            var rnd = new Random();
            if (rnd.Next(1, 13) % 2 == 0) _userManager.AddToRoleAsync(userIdentity, "user");
            else _userManager.AddToRoleAsync(userIdentity, "regular");
            //End

            if (!resultIdentity.Result.Succeeded) return Result.Fail("Deu ruim ao cadastrar usuário");

            var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
            var encodedCode = HttpUtility.UrlEncode(code);

            _emailService.SendEmail(new[] { userIdentity.Email }, "Link de Ativação", userIdentity.Id, encodedCode);
            return Result.Ok().WithSuccess(code);
        }

        public Result ActivateUserAccount(ActivateAccountRequest request)
        {
            var customIdentityUser = _userManager.Users.FirstOrDefault(u => u.Id == request.UserId);
            var identityResult = _userManager.ConfirmEmailAsync(customIdentityUser, request.ActivateCode).Result;
            return identityResult.Succeeded ? Result.Ok() : Result.Fail("Deu ruim ao ativar conta de usuário");
        }
    }
}