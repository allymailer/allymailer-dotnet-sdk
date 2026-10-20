using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.ConfirmedEmail.Model;

/// <summary>
/// ConfirmedEmailDeleteResponse
/// </summary>
public class ConfirmedEmailDeleteResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
