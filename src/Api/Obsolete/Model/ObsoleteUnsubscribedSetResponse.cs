using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedSetResponse
/// </summary>
public class ObsoleteUnsubscribedSetResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Відписаний email.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Подробиці відписки:
    /// <para/>
    /// - "unsubscribed" якщо email було щойно відписано,<br/>
    /// - "was inactive" якщо він уже був позначений як неактивний і немає сенсу відписувати,<br/>
    /// - "was unsubscribed" якщо email уже було відписано раніше,<br/>
    /// - "added and unsubscribed" якщо для цього email не зберігалося жодного більш раннього статусу, тому його було додано й одразу ж відписано.
    /// </summary>
    [JsonPropertyName("message")]
    public ObsoleteUnsubscribedSetMessage? Message { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
