using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Suppression.Model;

/// <summary>
/// SuppressionListRequest
/// </summary>
public class SuppressionListRequest
{
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
    /// Дата у форматі РРРР-ММ-ДД гг:хх:сс, щоб отримати список блокування з "start_time" по сьогоднішній<br/>
    /// день. Ігнорується, якщо параметр “cursor” не порожній.
    /// </summary>
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    /// <summary>
    /// Параметр указує, з якої позиції починати вибірку. Має бути порожнім або відсутнім для отримання<br/>
    /// першої порції даних; для отримання другої та наступних порцій його необхідно задавати, підставляючи<br/>
    /// значення поля "cursor" з попередньої порції.
    /// </summary>
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }

    /// <summary>
    /// Максимум записів, що видаються за один раз, за замовчуванням 50.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
