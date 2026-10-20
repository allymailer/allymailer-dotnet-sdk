using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete.Model;

/// <summary>
/// ObsoleteUnsubscribedListResponseUnsubscribedItem
/// </summary>
public class ObsoleteUnsubscribedListResponseUnsubscribedItem
{
    /// <summary>
    /// Відписаний email.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Дата й час відписки адреси у форматі РРРР-ММ-ДД гг:хх:сс у часовому поясі UTC.
    /// </summary>
    [JsonPropertyName("unsubscribed_on")]
    public string? UnsubscribedOn { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
