using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.ViewModels.Acomodacao
{
    public class EditAcomodacaoViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Este campo deve conter entre {1} e {0} caracteres")]
        public string DescricaoDaAcomodacao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {1} e {0} caracteres")]
        public string Anotacoes { get; set; }
    }
}