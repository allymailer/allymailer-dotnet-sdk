using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Suppression.Model;

/// <summary>
/// Об'єкт з даними про блокування email.
/// </summary>
public class SuppressionListObject
{
    /// <summary>
    /// Email-адреса, яка знаходиться в списку недоступних.
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
    /// - __complained__ - в одному з попередніх листів адресат натиснув “Це спам”.<br/>
    /// - __blocked__ - надсилання на цю адресу заборонено адміністрацією AllyMailer.<br/>
    /// У майбутньому можуть з'явитися й інші причини.
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

    /// <summary>
    /// Джерело потрапляння до списку недоступних. Одне з таких:
    /// <para/>
    /// - __user__ - дії користувача за допомогою методу [suppression/set](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-set);<br/>
    /// - __system__ - надсилання на цю адресу заборонено системою, наприклад через багаторазові недоставки;<br/>
    /// - __subscriber__ - у зв'язку з діями отримувача. В одному з попередніх листів адресат натиснув “Це спам” або відписався.
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Чи можна видалити запис за допомогою виклику suppression/delete.
    /// </summary>
    [JsonPropertyName("is_deletable")]
    public bool? IsDeletable { get; set; }

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
