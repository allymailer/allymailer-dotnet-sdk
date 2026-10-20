using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedSetRequest
/// </summary>
public class ObsoleteUnsubscribedSetRequest
{
    /// <summary>
    /// Email, який має бути відписано.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
