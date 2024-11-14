using System.Text.Json.Serialization;

namespace Noknok.Integration.Dynamics365.Models;

public class ODataResponse<TValue>
{
    [JsonPropertyName("@odata.nextLink")]
    public string? OdataNextLink { get; set; }
    public List<TValue> Value { get; set; } = [];
}