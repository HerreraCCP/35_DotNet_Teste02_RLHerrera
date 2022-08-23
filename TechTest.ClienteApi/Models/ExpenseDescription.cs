using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class ExpenseDescription
    {
        public int Id { get; set; }

        public string DescriptionOfExpensive { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}