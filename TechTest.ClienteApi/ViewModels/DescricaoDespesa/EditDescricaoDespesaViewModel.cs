using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClienteApi.ViewModels.DescricaoDespesa
{
    public class EditDescricaoDespesaViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descricao é obrigatória")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Este campo deve conter entre {1} e {0} caracteres")]
        public string DescricaoTipoDeDepesa { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {1} e {0} caracteres")]
        public string Anotacoes { get; set; }
    }
}