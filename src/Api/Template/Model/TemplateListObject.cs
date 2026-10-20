using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Template.Model;

/// <summary>
/// TemplateListObject
/// </summary>
public class TemplateListObject
{
    /// <summary>
    /// Унікальний ідентифікатор шаблону.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Назва шаблону.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Тип редактора шаблону у вебінтерфейсі, "html" або "visual". За замовчуванням — "html". Створення<br/>
    /// шаблонів із типом редактора "visual" можливе лише через вебінтерфейс.
    /// </summary>
    [JsonPropertyName("editor_type")]
    public EditorType? EditorType { get; set; }

    /// <summary>
    /// Параметр для вибору [шаблонізатора](https://docs.allymailer.com.ua/en/template-engines) — або<br/>
    /// "simple", або "velocity", або "liquid", або "none". За замовчуванням — "simple".
    /// </summary>
    [JsonPropertyName("template_engine")]
    public TemplateEngine? TemplateEngine { get; set; }

    /// <summary>
    /// Об'єкт для передачі глобальних підстановок (наприклад, назва компанії). Якщо назви змінних<br/>
    /// повторюються в об'єкті користувацьких підстановок "substitutions", значення змінних будуть взяті з<br/>
    /// об'єкта "substitutions". Підстановки працюють у параметрах:
    /// <para/>
    /// - body.html<br/>
    /// - body.plaintext<br/>
    /// - body.amp<br/>
    /// - subject<br/>
    /// - from_name<br/>
    /// - options.unsubscribe_url<br/>
    /// Назви змінних можуть містити літери латинського алфавіту, цифри, символ "_". Першим символом може<br/>
    /// бути лише літера, використання пробілів не допускається.
    /// </summary>
    [JsonPropertyName("global_substitutions")]
    public Dictionary<string, string>? GlobalSubstitutions { get; set; }

    /// <summary>
    /// Об'єкт для передачі метаданих, спільних для всіх отримувачів, у форматі: "key": "value". Максимальна<br/>
    /// кількість ключів: 10; максимальна довжина ключа: 64 символи; максимальна довжина значення ключа:<br/>
    /// 1024 символи. Метадані передаються під час виклику<br/>
    /// [webhook](https://docs.allymailer.com.ua/ua/web-api-ref#webhook). Для групування листів в одну<br/>
    /// розсилку може використовуватися системний ключ "campaign_id" зі значенням у вигляді невід'ємного<br/>
    /// десяткового цілого до 128 біт або UUID вигляду "c7703772-453e-4c77-a9ae-9a1b94051b5a". Неправильні<br/>
    /// значення будуть прирівняні до '0'
    /// </summary>
    [JsonPropertyName("global_metadata")]
    public Dictionary<string, string>? GlobalMetadata { get; set; }

    /// <summary>
    /// Об'єкт, що містить у собі html-, plaintext- і amp-частини листа. Або html-, або plaintext-частина<br/>
    /// має бути присутня обов'язково.
    /// </summary>
    [JsonPropertyName("body")]
    public BodyObject? Body { get; set; }

    /// <summary>
    /// Тема листа.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Email-адреса відправника.
    /// </summary>
    [JsonPropertyName("from_email")]
    public string? FromEmail { get; set; }

    /// <summary>
    /// Ім'я відправника.
    /// </summary>
    [JsonPropertyName("from_name")]
    public string? FromName { get; set; }

    /// <summary>
    /// Необов'язкова email-адреса для відповідей (на випадок, якщо відрізняється від адреси відправника)
    /// </summary>
    [JsonPropertyName("reply_to")]
    public string? ReplyTo { get; set; }

    /// <summary>
    /// Необов'язкове ім'я для відповідей (якщо вказано email reply_to і ви хочете, щоб відображався не лише<br/>
    /// цей email, але й ім'я)
    /// </summary>
    [JsonPropertyName("reply_to_name")]
    public string? ReplyToName { get; set; }

    /// <summary>
    /// 1 = відстеження переходів за посиланнями увімкнено (значення за замовчуванням), 0=вимкнено. Для<br/>
    /// використання track_links = 0 вам потрібно [попросити<br/>
    /// техпідтримку](https://cp.allymailer.com/ua/support) увімкнути таку можливість.<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("track_links")]
    public int? TrackLinks { get; set; }

    /// <summary>
    /// 1 = відстеження прочитань листа увімкнено (значення за замовчуванням), 0 = вимкнено. Для<br/>
    /// використання track_read = 0 вам потрібно [попросити<br/>
    /// техпідтримку](https://cp.allymailer.com/ua/support) увімкнути таку можливість.<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("track_read")]
    public int? TrackRead { get; set; }

    /// <summary>
    /// Об'єкт, що містить додаткові заголовки листа, максимум 50 заголовків. Префікс “X-” в імені заголовка<br/>
    /// обов'язковий, заголовки без нього будуть проігноровані, наприклад X-ALLYMAILER-Global-Language,<br/>
    /// X-ALLYMAILER-Template-Engine. Стандартні заголовки "To", "CC", "BCC" передаються без префікса "X-",<br/>
    /// але обробляються спеціальним чином, тому мають низку обмежень, докладніше про це можна прочитати<br/>
    /// [тут](/cc-and-bcc). Також якщо [техпідтримка](https://cp.allymailer.com/ua/support) схвалила вам<br/>
    /// вимкнення посилання відписки, то ви також можете передавати заголовки List-Unsubscribe,<br/>
    /// List-Subscribe, List-Help, List-Owner, List-Archive, In-Reply-To і References.
    /// </summary>
    [JsonPropertyName("headers")]
    public HeadersObject? Headers { get; set; }

    /// <summary>
    /// Необов'язковий масив вкладень.
    /// </summary>
    [JsonPropertyName("attachments")]
    public List<AttachmentsArrayItem>? Attachments { get; set; }

    /// <summary>
    /// Масив для передачі inline-вкладень, наприклад, для включення зображень у лист замість завантаження<br/>
    /// їх за посиланням.
    /// </summary>
    [JsonPropertyName("inline_attachments")]
    public List<InlineAttachmentsArrayItem>? InlineAttachments { get; set; }

    /// <summary>
    /// Час створення шаблону в часовому поясі UTC у форматі "РРРР-ММ-ДД гг:хх:сс"
    /// </summary>
    [JsonPropertyName("created")]
    public string? Created { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор користувача.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор проєкту, ASCII-рядок до 36 символів завдовжки.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Назва проєкту, унікальна в межах облікового запису користувача.
    /// </summary>
    [JsonPropertyName("project_name")]
    public string? ProjectName { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
