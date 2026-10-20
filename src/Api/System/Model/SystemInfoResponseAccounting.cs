using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.System.Model;

/// <summary>
/// Об'єкт з властивостями періоду обліку. Присутній лише якщо API-ключ, використаний для запиту, був<br/>
/// API-ключем користувача.
/// </summary>
public class SystemInfoResponseAccounting
{
    /// <summary>
    /// Дата й час початку періоду обліку в часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс".
    /// </summary>
    [JsonPropertyName("period_start")]
    public string? PeriodStart { get; set; }

    /// <summary>
    /// Дата й час кінця періоду обліку в часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс".
    /// </summary>
    [JsonPropertyName("period_end")]
    public string? PeriodEnd { get; set; }

    /// <summary>
    /// Кількість листів, включених в обліковий період.
    /// </summary>
    [JsonPropertyName("emails_included")]
    public long? EmailsIncluded { get; set; }

    /// <summary>
    /// Кількість листів, надісланих за обліковий період. Може бути більшою за emails_included у разі<br/>
    /// перевищення.
    /// </summary>
    [JsonPropertyName("emails_sent")]
    public long? EmailsSent { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
