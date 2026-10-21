using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
    /// цьому отримувачу — див. [Шаблонізатори](https://docs.allymailer.com.ua/ua/template-engines)).<br/>
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

    private static readonly Regex AddressPattern = new Regex("^(.*)<([^>]+)>$", RegexOptions.Singleline);

    /// <summary>
    /// A recipient from "email" or "Name &lt;email&gt;": the name goes to the to_name substitution (the name in the
    /// "To" header).
    /// </summary>
    /// <param name="address">"email" or "Name &lt;email&gt;".</param>
    /// <param name="substitutions">More substitutions of the recipient.</param>
    public static MessageObjectRecipientsItem FromAddress(string address, IDictionary<string, string>? substitutions = null)
    {
        var email = address.Trim();
        var name = "";
        var match = AddressPattern.Match(email);
        if (match.Success)
        {
            email = match.Groups[2].Value.Trim();
            name = match.Groups[1].Value.Trim().Trim('"');
        }
        var all = substitutions == null ? null : new Dictionary<string, string>(substitutions);
        if (name.Length > 0)
        {
            all = new Dictionary<string, string> { ["to_name"] = name };
            if (substitutions != null)
            {
                foreach (var entry in substitutions)
                {
                    all[entry.Key] = entry.Value;  // an explicit to_name wins
                }
            }
        }
        return new MessageObjectRecipientsItem { Email = email, Substitutions = all };
    }

    /// <summary>Sets the metadata of the recipient (returned in its webhook events).</summary>
    /// <param name="metadata">The metadata.</param>
    /// <returns>This recipient.</returns>
    public MessageObjectRecipientsItem WithMetadata(IDictionary<string, string> metadata)
    {
        Metadata = new Dictionary<string, string>(metadata);
        return this;
    }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
