using System.Linq;
using FluentResults;
using UsuariosApi.Data.Requests;
using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);

            if (!resultadoIdentity.Result.Succeeded) return Result.Fail("Login falhou");

            var identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper());
            return Result.Ok().WithSuccess(_tokenService.CreateToken(identityUser).Value);
        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            var identityUser = RecuperaUsuarioPorEmail(request.Email);
            return _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result
                .Succeeded
                ? Result.Ok().WithSuccess("Senha redefinida com sucesso!")
                : Result.Fail("Houve um erro na operação");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            var identityUser = RecuperaUsuarioPorEmail(request.Email);
            return identityUser == null
                ? Result.Fail("Falha ao solicitar redefinição")
                : Result.Ok()
                    .WithSuccess(_signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result);
        }

        private IdentityUser<int> RecuperaUsuarioPorEmail(string email) =>
            _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
    }
}