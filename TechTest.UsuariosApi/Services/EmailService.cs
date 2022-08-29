using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using UsuariosApi.Models;

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

        private MimeMessage CreateEmailBody(Message message)
        {
            MimeMessage emailMessage = new();
            emailMessage.From.Add(
                address: MailboxAddress.Parse(_configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.Recipient);
            emailMessage.Subject = message.Topic;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        private void Send(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), true);
                client.AuthenticationMechanisms.Remove("XOUATH2");
                client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));
                client.Send(emailMessage);
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

        public void SendEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            Message message = new(destinatario, assunto, usuarioId, code);
            var emailMessage = CreateEmailBody(message);
            Send(emailMessage);
        }
    }
}