using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Suppression.Model;

/// <summary>
/// SuppressionGetRequest
/// </summary>
public class SuppressionGetRequest
{
    /// <summary>
    /// Email-адреса, для якої потрібно отримати дані про недоступність.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Якщо у користувача увімкнено проєкти, all_projects=true дозволить шукати за даними всіх проєктів.<br/>
    /// За замовчуванням: `False`.
    /// </summary>
    [JsonPropertyName("all_projects")]
    public bool? AllProjects { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
