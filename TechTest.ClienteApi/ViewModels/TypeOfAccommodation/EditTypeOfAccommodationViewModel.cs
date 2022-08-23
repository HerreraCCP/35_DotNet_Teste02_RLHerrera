using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClienteApi.ViewModels.TypeOfAccommodation
{
    public class EditTypeOfAccommodationViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Este campo deve conter entre {1} e {0} caracteres")]
        public string DescriptionOfAccommodation { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {1} e {0} caracteres")]
        public string Notes { get; set; }
    }
}
