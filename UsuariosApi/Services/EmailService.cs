using MimeKit;
using MailKit.Net.Smtp;
using UsuariosApi.Models;
using UsuariosApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public EmailService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            MimeMessage mensagemDeEmail = new();
            mensagemDeEmail.From.Add(
                address: MailboxAddress.Parse(_configuration.GetValue<string>("EmailSettings:From")));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }

        public void Enviar(MimeMessage mensagemDeEmail)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                    _configuration.GetValue<int>("EmailSettings:Port"), true);
                client.AuthenticationMechanisms.Remove("XOUATH2");
                client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                    _configuration.GetValue<string>("EmailSettings:Password"));
                client.Send(mensagemDeEmail);
            }
            catch
            {
                _logger.LogWarning("Deu ruim ao enviar o email service");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new(destinatario, assunto, usuarioId, code);
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }
    }
}