using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Email.Model;

/// <summary>
/// Об'єкт, що містить усі властивості повідомлення, яке надсилається.
/// </summary>
public class MessageObject
{
    /// <summary>
    /// Масив об'єктів з email-адресами, підстановками та метаданими отримувачів.
    /// </summary>
    [JsonPropertyName("recipients")]
    public List<MessageObjectRecipientsItem>? Recipients { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор шаблону, створеного раніше методом<br/>
    /// [template/set](https://docs.allymailer.com.ua/ua/web-api-ref#template-set). Якщо вказано, то поля<br/>
    /// шаблону підставляються замість пропущених полів email/send. Наприклад, якщо в email/send не вказано<br/>
    /// body — береться тіло листа із шаблону, а якщо не вказано subject — береться тема із шаблону.
    /// </summary>
    [JsonPropertyName("template_id")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Масив із рядків, до 100 символів у кожному. Максимум 4 рядки в масиві, рядки мають бути унікальними.<br/>
    /// Для кожного облікового запису/проєкту дозволено не більше ніж 10 000 міток, у разі перевищення метод<br/>
    /// email/send повертатиме помилку. Мітки можуть використовуватися для категоризації листів за обраними<br/>
    /// вами критеріями. Мітки передаються методами event-dump.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Пропустити чи не пропускати додавання стандартного блока з посиланням відписки до HTML-частини<br/>
    /// листа. 1=пропустити, 0=додати, за замовчуванням 0. Для использования skip_unsubscribe=1 вам надо<br/>
    /// [попросить техподдержку](https://cp.allymailer.com/ru/support) включить такую возможность.<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("skip_unsubscribe")]
    public int? SkipUnsubscribe { get; set; }

    /// <summary>
    /// Заголовок для вибору мови посилання та сторінки відписки. Припустимі значення "be", "de", "en",<br/>
    /// "es", "fr", "it", "pl", "pt", "ru", "ua", "kz".
    /// </summary>
    [JsonPropertyName("global_language")]
    public string? GlobalLanguage { get; set; }

    /// <summary>
    /// Параметр для вибору [шаблонізатора](https://docs.allymailer.com.ua/ua/template-engines) — або<br/>
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
    /// Email-адреса відправника. Обов'язковий лише за порожнього template_id.
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
    /// (0 або 1, за замовчуванням 0) Значення 1=ігнорувати список глобальної недоступності, під час<br/>
    /// надсилання не враховуватимуться дані про недоступність адрес або скарги, отримані під час надсилання<br/>
    /// іншими користувачами AllyMailer. Для деяких системних записів може бути заборонено<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_global")]
    public int? BypassGlobal { get; set; }

    /// <summary>
    /// (0 або 1, за замовчуванням 0) Значення 1=ігнорувати список недоступних адрес поточного облікового<br/>
    /// запису/проєкту. Указання 1 можливе лише за одночасного вказання bypass_global=1.<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_unavailable")]
    public int? BypassUnavailable { get; set; }

    /// <summary>
    /// (0 або 1, за замовчуванням 0) Значення 1=ігнорувати список відписаних від розсилок поточного<br/>
    /// облікового запису/проєкту. Указання 1 можливе лише за одночасного вказання bypass_global=1. Параметр<br/>
    /// доступний лише тим користувачам, яким [техпідтримка](https://cp.allymailer.com/ua/support) схвалила<br/>
    /// вимкнення посилання відписки.<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_unsubscribed")]
    public int? BypassUnsubscribed { get; set; }

    /// <summary>
    /// (0 або 1, за замовчуванням 0) Значення 1=ігнорувати список тих, хто поскаржився на обліковий<br/>
    /// запис/проєкт. Указання 1 можливе лише за одночасного вказання bypass_global=1. Параметр доступний<br/>
    /// лише тим користувачам, яким [техпідтримка](https://cp.allymailer.com/ua/support) схвалила вимкнення<br/>
    /// посилання відписки.<br/>
    /// Допустимі значення: `0`, `1`.
    /// </summary>
    [JsonPropertyName("bypass_complained")]
    public int? BypassComplained { get; set; }

    /// <summary>
    /// Рядок до 64 символів завдовжки з унікальним ключем запиту. Може використовуватися для запобігання<br/>
    /// випадковому дублюванню листів. У разі повторного надсилання запиту з тим самим ключем протягом<br/>
    /// хвилини запит буде відхилено. Може створюватися автоматично для кожного листа; щоб увімкнути<br/>
    /// автоматичну генерацію ключа, зв'яжіться з [техпідтримкою](https://cp.allymailer.com/ua/support).
    /// </summary>
    [JsonPropertyName("idempotence_key")]
    public string? IdempotenceKey { get; set; }

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
    /// Додаткові опції.
    /// </summary>
    [JsonPropertyName("options")]
    public MessageObjectOptions? Options { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
