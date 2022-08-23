using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ClienteApi.ViewModels.Expense
{
    public class ListExpenseViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int UserId { get; set; }

        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [DisplayName("Data da expiração")]
        public DateTime ExpireIn { get; set; }

        [DisplayName("Código de barras")]
        public string BarCode { get; set; }

        [DisplayName("Data da criação")]
        public DateTime CreatedIn { get; set; } = DateTime.UtcNow;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}
