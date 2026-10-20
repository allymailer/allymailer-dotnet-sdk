using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// DomainListRequest
/// </summary>
public class DomainListRequest
{
    /// <summary>
    /// Опціональне ім'я домену, якщо вам потрібно отримати статуси лише за одним доменом.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Максимальна кількість доменів, що повертаються. За замовчуванням 50.<br/>
    /// За замовчуванням: `50`.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Індекс першого домену зі списку, що повертається, рахуючи з нуля.<br/>
    /// За замовчуванням: `0`.
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
