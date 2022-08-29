using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class Despesa
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataExpiracao { get; set; }

        public string CodigoDeBarras { get; set; }

        public DateTime CriadoEm { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Anotacoes { get; set; }

        public DescricaoDespesa DescricaoDespesa { get; set; }

        public Acomodacao Acomodacao { get; set; }

        public int UserId { get; set; }

        public IList<Arquivo> Arquivos { get; set; }
    }
}