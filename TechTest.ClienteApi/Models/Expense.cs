using System;
using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class Expense : Entity
    {
        public int UserId { get; set; }

        public decimal Value { get; set; }

        public DateTime ExpireIn { get; set; }

        public string BarCode { get; set; }

        public DateTime CreatedIn { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }

        public ExpenseDescription ExpenseDescription { get; set; }

        public TypeOfLocation TypeOfLocation { get; set; }

        public TypeOfAccommodation TypeOfAccommodation { get; set; }
    }
}