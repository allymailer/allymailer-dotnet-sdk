using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// Об'єкт із властивостями фільтра дампа подій.
/// </summary>
public class FilterObject
{
    /// <summary>
    /// Ідентифікатор завдання, раніше повернутий методом<br/>
    /// [email/send](https://docs.allymailer.com.ua/ua/web-api-ref#email-send).
    /// </summary>
    [JsonPropertyName("job_id")]
    public string? JobId { get; set; }

    /// <summary>
    /// Статус email. Можна вказати одне з таких значень:
    /// <para/>
    /// __accepted__ — повідомлення прийнято, але ще не надіслано.
    /// <para/>
    /// __sent__ — повідомлення надіслано, але ще не доставлено.
    /// <para/>
    /// __delivered__ — повідомлення доставлено.
    /// <para/>
    /// __opened__ — повідомлення доставлено й зареєстровано його прочитання.
    /// <para/>
    /// __clicked__ — повідомлення доставлено, прочитано, було зареєстровано перехід за одним з посилань у<br/>
    /// листі.
    /// <para/>
    /// __unsubscribed__ — повідомлення було доставлено отримувачу й прочитано ним, але користувач<br/>
    /// відписався за посиланням у листі.
    /// <para/>
    /// __subscribed__ — повідомлення було доставлено отримувачу й прочитано ним, але користувач відписався,<br/>
    /// а потім знову підписався за посиланням у листі.
    /// <para/>
    /// __soft_bounced__ — тимчасова недоставка повідомлення. AllyMailer продовжує спроби доставки протягом<br/>
    /// 2 діб.
    /// <para/>
    /// __hard_bounced__ — доставка не вдалася, спроб доставки більше не буде.
    /// <para/>
    /// __spam__ — повідомлення доставлено, але позначено як “спам” отримувачем.
    /// </summary>
    [JsonPropertyName("status")]
    public EmailStatus? Status { get; set; }

    /// <summary>
    /// Внутрішній детальний статус доставки AllyMailer. Можна вказати декілька статусів через кому. Ось<br/>
    /// деякі з можливих статусів:
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
    /// Адреса отримувача
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Адреса відправника
    /// </summary>
    [JsonPropertyName("email_from")]
    public string? EmailFrom { get; set; }

    /// <summary>
    /// Домен отримувача
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Ідентифікатор розсилки, невід'ємне десяткове ціле або UUID до 128 біт, передане в метаданих у полі з<br/>
    /// іменем "campaign_id" (ім'я можна налаштувати через техпідтримку).
    /// </summary>
    [JsonPropertyName("campaign_id")]
    public string? CampaignId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
