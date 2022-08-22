using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class TypeOfLocation : Entity
    {
        public string DescriptionOfLocation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}