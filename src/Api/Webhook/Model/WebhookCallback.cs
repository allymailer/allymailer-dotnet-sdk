using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Опис формату webhook.
/// </summary>
public class WebhookCallback
{
    /// <summary>
    /// MD5-хеш рядкового тіла повідомлення, у якому значення auth замінено на api_key користувача/проєкту,<br/>
    /// чий обробник викликається. За допомогою цього отримувач сповіщення може як провести автентифікацію,<br/>
    /// так і перевірити цілісність сповіщення.
    /// </summary>
    [JsonPropertyName("auth")]
    public string? Auth { get; set; }

    /// <summary>
    /// Масив з одним елементом, що містить дані про події користувача/проєкту.
    /// </summary>
    [JsonPropertyName("events_by_user")]
    public List<WebhookCallbackEventsByUserItem>? EventsByUser { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
