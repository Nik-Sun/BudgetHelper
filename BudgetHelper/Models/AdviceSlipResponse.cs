using System.Text.Json.Serialization;

namespace BudgetHelper.Models
{

    public class AdviceSlipResponse
    {
        [JsonPropertyName("slip")]
        public Slip Slip { get; set; }
        public DateTime Created { get; set; }
    }

    public class Slip
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("advice")]
        public string Advice { get; set; }
    }

}
