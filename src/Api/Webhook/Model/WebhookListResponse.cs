using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// WebhookListResponse
/// </summary>
public class WebhookListResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Масив об'єктів з даними про webhook.
    /// </summary>
    [JsonPropertyName("objects")]
    public List<WebhookObject>? Objects { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
