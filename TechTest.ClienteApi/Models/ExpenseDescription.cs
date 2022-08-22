using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class ExpenseDescription : Entity
    {
        public string DescriptionOfExpensive { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}