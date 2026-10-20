using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Об'єкт з усіма властивостями webhook.
/// </summary>
public class WebhookObject
{
    /// <summary>
    /// Унікальний ідентифікатор webhook.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// URL webhook.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Статус webhook, за замовчуванням "active". Статус "disabled" означає, що webhook було вимкнено<br/>
    /// користувачем, "stopped" означає, що webhook було зупинено системою після 24 годин невдалих викликів<br/>
    /// (мінімум за 10 різними подіями).<br/>
    /// За замовчуванням: `active`.
    /// </summary>
    [JsonPropertyName("status")]
    public WebhookStatus? Status { get; set; }

    /// <summary>
    /// Формат сповіщень. "json_post"(за замовчуванням) або "json_post_gzip".<br/>
    /// За замовчуванням: `json_post`.
    /// </summary>
    [JsonPropertyName("event_format")]
    public WebhookEventFormat? EventFormat { get; set; }

    /// <summary>
    /// Повертати (1) чи ні (0) докладну інформацію про доставку. Якщо delivery_info=1, повертається<br/>
    /// додаткова інформація про відповідь сервера та внутрішній статус доставки в разі статусів доставки<br/>
    /// "hard_bounced" і "soft_bounced"; user agent, IP-адреса отримувача, геодані, визначена за user agent<br/>
    /// інформація про пристрій (тип і марка пристрою, назва ОС, браузера або поштового клієнта) і<br/>
    /// traffic_type (human, machine, protected або unknown) в разі статусів "opened" і "clicked"; URL для<br/>
    /// статусу "clicked". Для статусів "delivered", "soft_bounced" і "hard_bounced" AllyMailer також може<br/>
    /// повернути sender_ip — IP-адресу SMTP-сервера, з якого було надіслано лист, якщо він доступний.<br/>
    /// Допустимі значення: `0`, `1`.<br/>
    /// За замовчуванням: `0`.
    /// </summary>
    [JsonPropertyName("delivery_info")]
    public int? DeliveryInfo { get; set; }

    /// <summary>
    /// 0=в одному виклику може надійти декілька подій, 1=лише одна подія на виклик (не рекомендується).<br/>
    /// Допустимі значення: `0`, `1`.<br/>
    /// За замовчуванням: `0`.
    /// </summary>
    [JsonPropertyName("single_event")]
    public int? SingleEvent { get; set; }

    /// <summary>
    /// Максимальна кількість дозволених паралельних запитів до вашого сервера. Чим більше ваш сервер може<br/>
    /// обробити — тим краще.<br/>
    /// За замовчуванням: `10`.
    /// </summary>
    [JsonPropertyName("max_parallel")]
    public int? MaxParallel { get; set; }

    /// <summary>
    /// Час останнього оновлення властивостей webhook у часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс".
    /// </summary>
    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

    /// <summary>
    /// Об'єкт, що описує події, про які потрібно сповіщати.
    /// </summary>
    [JsonPropertyName("events")]
    public WebhookEventsObject? Events { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
