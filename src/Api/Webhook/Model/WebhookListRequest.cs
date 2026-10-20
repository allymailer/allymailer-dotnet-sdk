using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// WebhookListRequest
/// </summary>
public class WebhookListRequest
{
    /// <summary>
    /// Максимальна кількість webhook, що повертаються за один виклик. Рекомендується 50.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Індекс першого webhook зі списку, що повертається, рахуючи з нуля.<br/>
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
