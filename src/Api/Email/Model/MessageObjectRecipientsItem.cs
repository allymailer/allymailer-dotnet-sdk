using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Email.Model;

/// <summary>
/// MessageObjectRecipientsItem
/// </summary>
public class MessageObjectRecipientsItem
{
    /// <summary>
    /// Email-адреса отримувача
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Об'єкт, що описує підстановки для конкретного отримувача (наприклад, ім'я, товар для показу саме<br/>
    /// цьому отримувачу — див. [Шаблонізатори](https://docs.allymailer.com.ua/en/template-engines)).<br/>
    /// Підстановки працюють у параметрах:
    /// <para/>
    /// - body.html<br/>
    /// - body.plaintext<br/>
    /// - body.amp<br/>
    /// - subject<br/>
    /// - from_name<br/>
    /// - headers["List-Unsubscribe"]<br/>
    /// - options.unsubscribe_url<br/>
    /// Назви змінних можуть містити літери латинського алфавіту, цифри, символ "_". Першим символом може<br/>
    /// бути лише літера, використання пробілів не допускається. Є спеціальна змінна "to_name", що дозволяє<br/>
    /// задати ім'я отримувача. У разі підстановки туди "Ім'я Прізвище" SMTP-заголовок “To” виглядатиме як<br/>
    /// "Ім'я Прізвище &lt;email@example.com&gt;". Довжина змінної "to_name" обмежена 78 символами.
    /// </summary>
    [JsonPropertyName("substitutions")]
    public Dictionary<string, string>? Substitutions { get; set; }

    /// <summary>
    /// Об'єкт для передачі метаданих, специфічних для окремого отримувача, де "key" — рядок до 64 символів<br/>
    /// завдовжки, "value" — рядок до 1024 символів завдовжки, максимальна кількість ключів — 10. Метадані<br/>
    /// передаються під час виклику [webhook](https://docs.allymailer.com.ua/ua/web-api-ref#webhook) і<br/>
    /// методів [event-dump](https://docs.allymailer.com.ua/ua/web-api-ref#event-dump). Для групування<br/>
    /// листів в одну розсилку може використовуватися системний ключ "campaign_id" зі значенням у вигляді<br/>
    /// невід'ємного десяткового цілого або UUID до 128 біт.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
