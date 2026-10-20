using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// DomainDeleteRequest
/// </summary>
public class DomainDeleteRequest
{
    /// <summary>
    /// Ім'я домену, який потрібно видалити.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
