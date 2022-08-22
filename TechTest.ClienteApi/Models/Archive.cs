using System.Text.Json.Serialization;

namespace ClienteApi.Models
{
    public class Archive : Entity
    {
        public int UserId { get; set; }
        
        public string File { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Notes { get; set; }
    }
}