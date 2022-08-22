using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Interfaces
{
    public interface IEmail
    {
        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code);

        public void Enviar(MimeMessage mensagemDeEmail);

        public MimeMessage CriaCorpoDoEmail(Message message);
    }
}