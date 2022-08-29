using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ClienteApi.ViewModels.DescricaoDespesa
{
    public class ListDescricaoDespesasViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [DisplayName("Descrição da despesa")]
        public string DescriptionOfExpensive { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}
