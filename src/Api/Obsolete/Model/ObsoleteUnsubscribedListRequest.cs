using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedListRequest
/// </summary>
public class ObsoleteUnsubscribedListRequest
{
    /// <summary>
    /// Дата у форматі РРРР-ММ-ДД, щоб отримати всі відписані адреси з "date_from" по сьогоднішній день.<br/>
    /// Якщо параметр не вказати, період за замовчуванням буде з 00:00 сьогоднішнього дня за UTC.
    /// </summary>
    [JsonPropertyName("date_from")]
    public string? DateFrom { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
