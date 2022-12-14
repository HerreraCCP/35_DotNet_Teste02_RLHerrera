using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result UserLogin(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);

            if (!resultadoIdentity.Result.Succeeded) return Result.Fail("Login falhou");

            var identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper());
            var roles = _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault();
            var token = _tokenService.CreateToken(identityUser, roles);
            return Result.Ok();
        }

        public Result ResetUserPassword(PerformResetRequest request)
        {
            var identityUser = RecuperaUsuarioPorEmail(request.Email);
            return _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result
                .Succeeded
                ? Result.Ok().WithSuccess("Senha redefinida com sucesso!")
                : Result.Fail("Houve um erro na operação");
        }

        public Result RequestResetUserPassword(ApplyResetRequest request)
        {
            var identityUser = RecuperaUsuarioPorEmail(request.Email);
            return identityUser == null
                ? Result.Fail("Falha ao solicitar redefinição")
                : Result.Ok().WithSuccess(_signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result);
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email) =>
            _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
    }
}