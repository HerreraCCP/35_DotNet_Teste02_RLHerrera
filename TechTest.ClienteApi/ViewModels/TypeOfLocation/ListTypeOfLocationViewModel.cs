using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClienteApi.ViewModels.TypeOfLocation
{
    public class ListTypeOfLocationViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [DisplayName("Descrição do local")]
        public string DescriptionOfLocation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}
