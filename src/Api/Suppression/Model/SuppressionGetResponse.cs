using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Suppression.Model;

/// <summary>
/// SuppressionGetResponse
/// </summary>
public class SuppressionGetResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Email-адреса, для якої було запитано дані про недоступність.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Масив об'єктів з інформацією про недоступність.
    /// </summary>
    [JsonPropertyName("suppressions")]
    public List<SuppressionGetObject>? Suppressions { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
