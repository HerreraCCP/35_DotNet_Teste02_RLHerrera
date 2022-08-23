using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class TypeOfAccommodation
    {
        public int Id { get; set; }

        public string DescriptionOfAccommodation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}