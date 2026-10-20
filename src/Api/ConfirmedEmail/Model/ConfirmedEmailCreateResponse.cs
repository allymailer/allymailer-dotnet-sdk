using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.ConfirmedEmail.Model;

/// <summary>
/// ConfirmedEmailCreateResponse
/// </summary>
public class ConfirmedEmailCreateResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("confirmed_email")]
    public ConfirmedEmail? ConfirmedEmail { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
