using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class Arquivo
    {
        public int Id { get; set; }

        public string File { get; set; }

        public bool IsDone { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Anotacoes { get; set; }

        public Despesa Despesas { get; set; }
    }
}