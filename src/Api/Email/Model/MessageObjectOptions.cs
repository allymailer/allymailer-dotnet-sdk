using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Email.Model;

/// <summary>
/// Додаткові опції.
/// </summary>
public class MessageObjectOptions
{
    /// <summary>
    /// Дата й час у форматі "РРРР-ММ-ДД гг:хх:сс" у часовому поясі UTC. Дозволяє запланувати час надсилання<br/>
    /// на майбутнє, у межах 24 годин від поточного часу.
    /// </summary>
    [JsonPropertyName("send_at")]
    public string? SendAt { get; set; }

    /// <summary>
    /// Користувацьке посилання відписки. Докладніше [тут](https://docs.allymailer.com.ua/unsubscribe-link).
    /// </summary>
    [JsonPropertyName("unsubscribe_url")]
    public string? UnsubscribeUrl { get; set; }

    /// <summary>
    /// Ідентифікатор backend-домену, через який надсилається повідомлення. Якщо відсутній — надсилання буде<br/>
    /// від імені backend-домену, призначеного вашому обліковому запису/проєкту за замовчуванням. Ви можете<br/>
    /// придбати один або декілька виділених IP-адрес і використовувати цю опцію для вибору.
    /// </summary>
    [JsonPropertyName("custom_backend_id")]
    public int? CustomBackendId { get; set; }

    /// <summary>
    /// Ідентифікатор пулу SMTP, через який надсилається повідомлення. Зазвичай немає необхідності вказувати<br/>
    /// пул явно, оскільки він обирається автоматично або як пул за замовчуванням для вашого облікового<br/>
    /// запису/проєкту, або відповідно до переданого параметра custom_backend_id.
    /// </summary>
    [JsonPropertyName("smtp_pool_id")]
    public string? SmtpPoolId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
