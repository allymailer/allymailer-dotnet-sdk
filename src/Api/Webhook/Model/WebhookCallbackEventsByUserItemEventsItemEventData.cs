using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Об'єкт з різними властивостями події, що залежать від поля "event_name". Нижче перелічено всі<br/>
/// можливі властивості, спочатку для події "transactional_email_status", потім для події<br/>
/// "transactional_spam_block".
/// </summary>
public class WebhookCallbackEventsByUserItemEventsItemEventData
{
    /// <summary>
    /// Ідентифікатор завдання, раніше повернутий методом<br/>
    /// [email/send](https://docs.allymailer.com.ua/ua/web-api-ref#email-send). Присутній лише якщо<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("job_id")]
    public string? JobId { get; set; }

    /// <summary>
    /// Метадані, раніше передані методом<br/>
    /// [email/send](https://docs.allymailer.com.ua/ua/web-api-ref#email-send) у полі recipients.metadata<br/>
    /// або global_metadata. Присутні лише якщо event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Email, на який було здійснено надсилання. Присутній лише якщо<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Статус доставки листа. Присутній лише якщо event_name="transactional_email_status". Набуває одного з<br/>
    /// таких значень:
    /// <para/>
    /// __accepted__ — повідомлення прийнято, але ще не надіслано.
    /// <para/>
    /// __sent__ — повідомлення надіслано, але ще не доставлено.
    /// <para/>
    /// __delivered__ — повідомлення доставлено. Може змінитися на “opened”, “clicked”, “unsubscribed”,<br/>
    /// “subscribed” або “spam”.
    /// <para/>
    /// __opened__ — повідомлення доставлено й зареєстровано його прочитання. Може змінитися на “clicked”,<br/>
    /// “unsubscribed”, “subscribed” або “spam”.
    /// <para/>
    /// __clicked__ — повідомлення доставлено, прочитано, було зареєстровано перехід за одним з посилань у<br/>
    /// листі. Може змінитися на “unsubscribed”, “subscribed” або “spam”.
    /// <para/>
    /// __unsubscribed__ — повідомлення було доставлено отримувачу й прочитано ним, але користувач<br/>
    /// відписався за посиланням у листі. Може змінитися на “subscribed”.
    /// <para/>
    /// __subscribed__ — повідомлення було доставлено отримувачу й прочитано ним, але користувач відписався,<br/>
    /// а потім знову підписався за посиланням у листі. Може змінитися на “unsubscribed”.
    /// <para/>
    /// __soft_bounced__ — тимчасова недоставка повідомлення. AllyMailer продовжує спроби доставки протягом<br/>
    /// 2 діб. У разі успішної доставки лист перейде у статус “delivered”, у разі неуспішної — у<br/>
    /// “hard_bounced”.
    /// <para/>
    /// __hard_bounced__ — доставка не вдалася, спроб доставки більше не буде. Статус остаточний. Можливих<br/>
    /// причин невдалої доставки безліч, для їх аналізу можна використати текст відповіді SMTP-сервера з<br/>
    /// поля delivery_info.destination_response або нашу внутрішню класифікацію помилок з поля<br/>
    /// delivery_info.delivery_status.
    /// <para/>
    /// __spam__ — повідомлення доставлено, але позначено як “спам” отримувачем. Статус остаточний. Ось<br/>
    /// деякі домени, від яких AllyMailer може отримати й обробити скаргу про спам (використовуючи<br/>
    /// технологію FBL): mail.ru, msn.com, outlook.com, hotmail.com, live.com, ukr.net, yahoo.com, aol.com.
    /// </summary>
    [JsonPropertyName("status")]
    public EmailStatus? Status { get; set; }

    /// <summary>
    /// Дата й час події в часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс". Присутній лише якщо<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("event_time")]
    public string? EventTime { get; set; }

    /// <summary>
    /// URL у разі статусів "opened" і "clicked". Присутній лише якщо<br/>
    /// event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Об'єкт з додатковою інформацією про доставку. Присутній лише якщо властивість delivery_info під час<br/>
    /// встановлення webhook дорівнювала 1 і якщо event_name="transactional_email_status".
    /// </summary>
    [JsonPropertyName("delivery_info")]
    public WebhookCallbackEventsByUserItemEventsItemEventDataDeliveryInfo? DeliveryInfo { get; set; }

    /// <summary>
    /// Дата й час спам-блока в часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс". Присутній лише якщо<br/>
    /// event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("block_time")]
    public string? BlockTime { get; set; }

    /// <summary>
    /// Тип спам-блока — заблоковано або один відправляючий SMTP, або одразу багато. У разі блокування<br/>
    /// одиночного SMTP із загального пулу AllyMailer пробуватиме ще декілька. Властивість присутня лише<br/>
    /// якщо event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("block_type")]
    public BlockType? BlockType { get; set; }

    /// <summary>
    /// Домен, що заблокував надсилання. Властивість присутня лише якщо<br/>
    /// event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Кількість заблокованих вихідних SMTP. Властивість присутня лише якщо<br/>
    /// event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("SMTP_blocks_count")]
    public int? SMTPBlocksCount { get; set; }

    /// <summary>
    /// Це подія блокування чи розблокування. Властивість присутня лише якщо<br/>
    /// event_name="transactional_spam_block".
    /// </summary>
    [JsonPropertyName("domain_status")]
    public DomainStatus? DomainStatus { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
