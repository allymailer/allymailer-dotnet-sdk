using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Suppression.Model;

/// <summary>
/// SuppressionSetRequest
/// </summary>
public class SuppressionSetRequest
{
    /// <summary>
    /// Email-адреса, яку потрібно додати до списку недоступних.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Причина, з якої надсилання на цей email блокується. Одна з таких:
    /// <para/>
    /// - __unsubscribed__ - зазначену адресу відписано;<br/>
    /// - __temporary_unavailable__ - адреса тимчасово недоступна. Це означає, що протягом наступних трьох діб надсилання на цю адресу повертатиме помилку. Email може бути тимчасово недоступним з різних причин, наприклад:
    /// <para/>
    /// - попереднє надсилання було відхилено сервером отримувача як спам;<br/>
    /// - поштова скринька отримувача переповнена або не використовується;<br/>
    /// - домен не приймає пошту через неправильне налаштування на боці отримувача або з інших причин;<br/>
    /// - сервер відправника було відхилено через блеклистинг;
    /// <para/>
    /// - __permanent_unavailable__ - адреса перманентно недоступна через багаторазові недоставки;<br/>
    /// - __complained__ - в одному з попередніх листів адресат натиснув “Це спам”.
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

    /// <summary>
    /// Коли було встановлено недоступність, у часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс".
    /// </summary>
    [JsonPropertyName("created")]
    public string? Created { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
