using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosApi.Models
{
    public class Message
    {
        public List<MailboxAddress> Recipient { get; set; }
        
        public string Topic { get; set; }
        
        public string Content { get; set; }
        
        public Message(IEnumerable<string> recipient, string topic,
            int userId, string code)
        {
            Recipient = new List<MailboxAddress>();
            Recipient.AddRange(recipient.Select(d => MailboxAddress.Parse(d)));
            Topic = topic;
            Content = $"http://localhost:6000/Active?UserId={userId}&ActivationCode={code}";
        }
    }
}