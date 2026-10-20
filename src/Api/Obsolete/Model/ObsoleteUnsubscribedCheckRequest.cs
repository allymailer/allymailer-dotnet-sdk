using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedCheckRequest
/// </summary>
public class ObsoleteUnsubscribedCheckRequest
{
    /// <summary>
    /// Email, який необхідно перевірити.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
