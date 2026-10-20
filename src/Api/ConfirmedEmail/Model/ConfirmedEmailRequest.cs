using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.ConfirmedEmail.Model;

/// <summary>
/// ConfirmedEmailRequest
/// </summary>
public class ConfirmedEmailRequest
{
    /// <summary>
    /// Email address to add or delete.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
