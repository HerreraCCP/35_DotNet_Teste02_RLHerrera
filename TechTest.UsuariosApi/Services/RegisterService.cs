using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class RegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public Result UserRegistration(CreateUserDto createDto)
        {
            var user = _mapper.Map<User>(createDto);
            var userIdentity = _mapper.Map<IdentityUser<int>>(user);
            var resultIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);
            var roleManagerResult = _roleManager.CreateAsync(new IdentityRole<int>("admin")).Result;
            var usuarioRoleResult = _userManager.AddToRolesAsync(userIdentity, new List<string> { "admin" }).Result;
            // var usuarioRoleResult = _userManager.AddToRolesAsync(userIdentity, "admin").Result;

            if (!resultIdentity.Result.Succeeded) return Result.Fail("Deu ruim ao cadastrar usuário");

            var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
            var encodedCode = HttpUtility.UrlEncode(code);

            _emailService.SendEmail(new[] { userIdentity.Email }, "Link de Ativação", userIdentity.Id, encodedCode);
            return Result.Ok().WithSuccess(code);
        }

        public Result ActivateUserAccount(ActivateAccountRequest request)
        {
            var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == request.UserId);
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.ActivateCode).Result;
            return identityResult.Succeeded ? Result.Ok() : Result.Fail("Deu ruim ao ativar conta de usuário");
        }
    }
}