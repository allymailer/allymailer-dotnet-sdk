using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Об'єкт, що описує події, про які потрібно сповіщати.
/// </summary>
public class WebhookEventsObject
{
    /// <summary>
    /// Якщо присутній, то надсилатимуться сповіщення про спам-блокування. У масиві має бути єдиний елемент<br/>
    /// із рядком "*".
    /// </summary>
    [JsonPropertyName("spam_block")]
    public List<string>? SpamBlock { get; set; }

    /// <summary>
    /// Якщо присутній, то надсилатимуться сповіщення про зміну статусу email. У масиві перелічуються ті<br/>
    /// статуси, про які надсилатимуться сповіщення.
    /// </summary>
    [JsonPropertyName("email_status")]
    public List<EmailStatus>? EmailStatus { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
