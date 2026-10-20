using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Об'єкт з додатковою інформацією про доставку. Присутній лише якщо властивість delivery_info під час<br/>
/// встановлення webhook дорівнювала 1 і якщо event_name="transactional_email_status".
/// </summary>
public class WebhookCallbackEventsByUserItemEventsItemEventDataDeliveryInfo
{
    /// <summary>
    /// Внутрішній детальний статус доставки AllyMailer. Ось деякі з можливих статусів:
    /// <para/>
    /// __err_user_unknown__ -- адреса не існує (і невідомо, чи існувала колись);
    /// <para/>
    /// __err_user_inactive__ -- адреса не використовується;
    /// <para/>
    /// __err_will_retry__ -- лист було тимчасово відхилено, пізніше буде здійснено повторну спробу<br/>
    /// доставки;
    /// <para/>
    /// __err_mailbox_discarded__ -- адресу видалено (тобто колись існувала, але тепер її немає);
    /// <para/>
    /// __err_mailbox_full__ -- поштова скринька переповнена;
    /// <para/>
    /// __err_spam_rejected__ -- лист відхилено як спам;
    /// <para/>
    /// __err_blacklisted__ -- лист було відхилено через наявність IP-адреси або домену відправника в<br/>
    /// якомусь із чорних списків;
    /// <para/>
    /// __err_too_large__ -- на думку сервера отримувача, лист перевищує допустимий розмір;
    /// <para/>
    /// __err_unsubscribed__ -- адресат раніше відписався від ваших розсилок;
    /// <para/>
    /// __err_unreachable__ -- адресу позначено як постійно недоступну через багаторазові помилки доставки<br/>
    /// на цю адресу;
    /// <para/>
    /// __err_skip_letter__ -- надсилання скасовано, оскільки email-адреса тимчасово недоступна;
    /// <para/>
    /// __err_domain_inactive__ -- домен не приймає пошту або не існує;
    /// <para/>
    /// __err_destination_misconfigured__ -- домен не приймає пошту через неправильне налаштування на боці<br/>
    /// отримувача, причому відповідь сервера містить інформацію про усувну причину (наприклад,<br/>
    /// використовується непрацюючий блеклист тощо);
    /// <para/>
    /// __err_delivery_failed__ -- доставка не вдалася з інших причин;
    /// <para/>
    /// __err_spam_skipped__ -- надсилання скасовано через блокування розсилки як спаму;
    /// <para/>
    /// __err_lost__ -- лист не було надіслано через неузгодженість його частин, або його було втрачено<br/>
    /// через збій на нашому боці. Відправник має повторно надіслати лист самостійно, оскільки оригінал не<br/>
    /// зберігся;
    /// <para/>
    /// Ми залишаємо за собою право змінювати й додавати внутрішні статуси, тому решту<br/>
    /// використовуваних наразі статусів перелічимо лише довідково, без пояснень:<br/>
    /// ok_accepted, ok_sent, ok_delivered, ok_read, ok_link_visited, ok_unsubscribed, ok_resubscribed,<br/>
    /// ok_spam_folder, ok_fbl,<br/>
    /// not_sent, skip_dup_unreachable, skip_dup_temp_unreachable, skip_dup_mailbox_full,<br/>
    /// err_spam_removed, err_resend, err_unknown, err_retry_letter, err_src_invalid,<br/>
    /// err_dest_invalid, err_not_available, err_internal, err_no_dns, err_no_smtp, err_giveup.
    /// </summary>
    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// Відповідь SMTP-сервера.
    /// </summary>
    [JsonPropertyName("destination_response")]
    public string? DestinationResponse { get; set; }

    /// <summary>
    /// Рядок user agent отримувача. Присутній лише якщо вдалося його визначити, для статусів "clicked" і<br/>
    /// "opened".
    /// </summary>
    [JsonPropertyName("user_agent")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Тип пристрою отримувача, визначений за user agent. Присутній для статусів "clicked" і "opened";<br/>
    /// дорівнює "unknown", якщо тип пристрою визначити не вдалося.
    /// </summary>
    [JsonPropertyName("device_type")]
    public DeviceType? DeviceType { get; set; }

    /// <summary>
    /// Марка пристрою отримувача, визначена за user agent. Присутня для статусів "clicked" і "opened", якщо<br/>
    /// її вдалося визначити. Для device_type "mobile" і "tablet" присутня завжди та дорівнює "unknown",<br/>
    /// якщо марку визначити не вдалося.
    /// </summary>
    [JsonPropertyName("device_brand")]
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Назва операційної системи пристрою отримувача, визначена за user agent. Присутня для статусів<br/>
    /// "clicked" і "opened"; дорівнює "unknown", якщо ОС визначити не вдалося.
    /// </summary>
    [JsonPropertyName("os_name")]
    public string? OsName { get; set; }

    /// <summary>
    /// Назва браузера, у якому було відкрито лист, визначена за user agent. Присутня для статусів "clicked"<br/>
    /// і "opened", лише якщо було визначено браузер, тому ніколи не надходить разом з email_client_name.
    /// </summary>
    [JsonPropertyName("browser_name")]
    public string? BrowserName { get; set; }

    /// <summary>
    /// Назва поштового клієнта, у якому було відкрито лист, визначена за user agent. Присутня для статусів<br/>
    /// "clicked" і "opened", лише якщо було визначено поштовий клієнт, тому ніколи не надходить разом з<br/>
    /// browser_name.
    /// </summary>
    [JsonPropertyName("email_client_name")]
    public string? EmailClientName { get; set; }

    /// <summary>
    /// Якість трафіку для статусів "opened" і "clicked". "human" — розпізнано звичайний браузер або<br/>
    /// поштовий клієнт без машинних ознак; "machine" — бот, сканер посилань або проксі безпеки; "protected"<br/>
    /// — механізм приватності скриньки/ОС, участь людини підтвердити не можна (Apple Mail Privacy<br/>
    /// Protection / iCloud Private Relay, Gmail Image Proxy; Apple MPP — це protected, не machine);<br/>
    /// "unknown" — User-Agent відсутній, пошкоджений або не розпізнаний.
    /// </summary>
    [JsonPropertyName("traffic_type")]
    public TrafficType? TrafficType { get; set; }

    /// <summary>
    /// IP-адреса отримувача. Присутня лише якщо вдалося її визначити, для статусів "clicked" і "opened".
    /// </summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }

    /// <summary>
    /// ISO 3166-1 alpha-2 код країни, визначений за IP-адресою отримувача. Присутній для статусів "clicked"<br/>
    /// і "opened".
    /// </summary>
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Країна, визначена за IP-адресою отримувача. Присутня для статусів "clicked" і "opened".
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Місто, визначене за IP-адресою отримувача. Присутнє для статусів "clicked" і "opened".
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// IP-адреса SMTP-сервера, з якого було надіслано лист. Присутня лише для статусів "delivered",<br/>
    /// "soft_bounced" і "hard_bounced", якщо доступна.
    /// </summary>
    [JsonPropertyName("sender_ip")]
    public string? SenderIp { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
