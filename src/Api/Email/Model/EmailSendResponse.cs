using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Email.Model;

/// <summary>
/// EmailSendResponse
/// </summary>
public class EmailSendResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Ідентифікатор завдання з надсилання, може виявитися корисним під час з'ясування причин збоїв.
    /// </summary>
    [JsonPropertyName("job_id")]
    public string? JobId { get; set; }

    /// <summary>
    /// Масив email-адрес, успішно прийнятих до надсилання.
    /// </summary>
    [JsonPropertyName("emails")]
    public List<string>? Emails { get; set; }

    /// <summary>
    /// Об'єкт із email-адресами, не прийнятими до надсилання. Email представлено іменем властивості, а його<br/>
    /// статус — значенням властивості, наприклад: {"email1@gmail.com": "temporary_unavailable"}. Можливі<br/>
    /// статуси адрес, на які не вдалося здійснити надсилання:
    /// <para/>
    /// - __unsubscribed__ — зазначену адресу відписано;<br/>
    /// - __invalid__ — адреса не існує або введена некоректно;<br/>
    /// - __duplicate__ — адреса повторюється в запиті, повторне надсилання на той самий email запобігнуто;<br/>
    /// - __temporary_unavailable__ — адреса тимчасово недоступна. Це означає, що протягом наступних трьох діб надсилання на цю адресу повертатиме помилку. Email може бути тимчасово недоступним з різних причин, наприклад:
    /// <para/>
    /// - попереднє надсилання було відхилено сервером отримувача як спам;<br/>
    /// - поштова скринька отримувача переповнена або не використовується;<br/>
    /// - домен не приймає пошту через неправильне налаштування на боці отримувача або з інших причин;<br/>
    /// - сервер відправника було відхилено через блеклистинг;
    /// <para/>
    /// - __permanent_unavailable__ — адреса перманентно недоступна через багаторазові недоставки;<br/>
    /// - __complained__ — в одному з попередніх листів адресат натиснув “Це спам”;<br/>
    /// - __blocked__ — надсилання на цю адресу заборонено адміністрацією AllyMailer.<br/>
    /// У майбутньому можуть з'явитися й інші статуси.
    /// </summary>
    [JsonPropertyName("failed_emails")]
    public Dictionary<string, string>? FailedEmails { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
