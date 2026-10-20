using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EmailValidation.Model;

/// <summary>
/// EmailValidationSingleResponse
/// </summary>
public class EmailValidationSingleResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Email, що перевіряється.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Можливі статуси:
    /// <para/>
    /// - "valid" - email-адреса дійсна,<br/>
    /// - "invalid" - email-адреса недійсна,<br/>
    /// - "suspicious" - email-адреса підозріла,<br/>
    /// - "unknown" - адресу не вдалося перевірити. Приймаючий сервер не відповів у межах припустимого часу перевірки.
    /// </summary>
    [JsonPropertyName("result")]
    public string? Result { get; set; }

    /// <summary>
    /// Можливі статуси:
    /// <para/>
    /// - "no_mx_record" - у домену відсутні MX-записи,<br/>
    /// - "syntax_error" - адреса не відповідає стандартам email,<br/>
    /// - "possible_typo" - можливо, адресу введено з помилкою,<br/>
    /// - "mailbox_not_found" - адреса не існує,<br/>
    /// - "global_suppression" - адреса перманентно недоступна в сервісі через багаторазові недоставки,<br/>
    /// - "disposable" - одноразовий email, зазвичай такі адреси приймають пошту лише декілька хвилин,<br/>
    /// - "role" - загальна адреса електронної пошти, найчастіше використовується групою людей і не належить конкретній особі,<br/>
    /// - "abuse" - адреси, з яких надходить велика кількість скарг і натискань на "спам", іноді навіть в автоматичному режимі,<br/>
    /// - "spamtrap" - адреси-спам-пастки, зазвичай розміщуються поштовими провайдерами у відкритому доступі, але ніколи не використовуються для підписки. Надсилання листів на такі адреси сильно шкодить репутації відправника,<br/>
    /// - "smtp_connection_failed" - SMTP-сервер недоступний — можливо, адресу введено з помилкою.
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

    /// <summary>
    /// Імовірність того, що адреса дійсна. Від 0 до 100 (100 - дійсна, 0 - недійсна).
    /// </summary>
    [JsonPropertyName("validity")]
    public int? Validity { get; set; }

    /// <summary>
    /// Локальна частина email-адреси (усе, що перед знаком "@").
    /// </summary>
    [JsonPropertyName("local_part")]
    public string? LocalPart { get; set; }

    /// <summary>
    /// Доменна частина email-адреси.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Чи є у домену MX-запис? true якщо є, false якщо відсутній.
    /// </summary>
    [JsonPropertyName("mx_found")]
    public bool? MxFound { get; set; }

    /// <summary>
    /// Бажана MX-запис домену.
    /// </summary>
    [JsonPropertyName("mx_record")]
    public string? MxRecord { get; set; }

    /// <summary>
    /// Можлива адреса, отримана після виправлення помилки (для cause=possible_typo).
    /// </summary>
    [JsonPropertyName("did_you_mean")]
    public string? DidYouMean { get; set; }

    /// <summary>
    /// Дата й час перевірки адреси у форматі РРРР-ММ-ДД гг:хх:сс у часовому поясі UTC.
    /// </summary>
    [JsonPropertyName("processed_at")]
    public string? ProcessedAt { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
