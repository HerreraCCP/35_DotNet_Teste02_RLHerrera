using ClienteApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ClienteApi.ViewModels.TypeOfAccommodation
{
    public class ListTypeOfAccommodationViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [DisplayName("Descrição da locação")]
        public string DescriptionOfAccommodation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}