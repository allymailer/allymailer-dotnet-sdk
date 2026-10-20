using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// WebhookCallbackEventsByUserItemEventsItem
/// </summary>
public class WebhookCallbackEventsByUserItemEventsItem
{
    /// <summary>
    /// Тип даних, що містяться в об'єкті event_data. Або "transactional_email_status", або<br/>
    /// "transactional_spam_block".
    /// </summary>
    [JsonPropertyName("event_name")]
    public EventName? EventName { get; set; }

    /// <summary>
    /// Об'єкт з різними властивостями події, що залежать від поля "event_name". Нижче перелічено всі<br/>
    /// можливі властивості, спочатку для події "transactional_email_status", потім для події<br/>
    /// "transactional_spam_block".
    /// </summary>
    [JsonPropertyName("event_data")]
    public WebhookCallbackEventsByUserItemEventsItemEventData? EventData { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
