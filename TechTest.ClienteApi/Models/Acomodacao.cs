using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class Acomodacao
    {
        public int Id { get; set; }

        public string DescricaoDaAcomodacao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Anotacoes { get; set; }

        public IList<Despesa> Despesas { get; set; }
    }
}