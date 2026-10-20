using System.Net.Http;
using AllyMailer.Api.ConfirmedEmail;
using AllyMailer.Api.Domain;
using AllyMailer.Api.Email;
using AllyMailer.Api.EmailValidation;
using AllyMailer.Api.EventDump;
using AllyMailer.Api.Obsolete;
using AllyMailer.Api.Project;
using AllyMailer.Api.Suppression;
using AllyMailer.Api.System;
using AllyMailer.Api.Tag;
using AllyMailer.Api.Template;
using AllyMailer.Api.Webhook;
using AllyMailer.Client;

namespace AllyMailer;

/// <summary>
/// Entry point of the AllyMailer API: one property per section.
/// </summary>
public class AllyMailerClient
{
    /// <param name="apiKey">User or project API key.</param>
    /// <param name="host">API host (Configuration.HostCp, "cp.allymailer.com") or a full base URL; null: the default one.</param>
    public AllyMailerClient(string apiKey, string? host = null)
        : this(new Configuration(apiKey) { BaseUrl = host == null ? Configuration.DefaultBaseUrl : Configuration.BaseUrlFor(host) })
    {
    }

    public AllyMailerClient(Configuration config, HttpClient? httpClient = null)
    {
        ApiClient = new ApiClient(config, httpClient);
        Email = new EmailApi(ApiClient);
        EmailValidation = new EmailValidationApi(ApiClient);
        ConfirmedEmail = new ConfirmedEmailApi(ApiClient);
        Template = new TemplateApi(ApiClient);
        Webhook = new WebhookApi(ApiClient);
        Suppression = new SuppressionApi(ApiClient);
        Domain = new DomainApi(ApiClient);
        EventDump = new EventDumpApi(ApiClient);
        Tag = new TagApi(ApiClient);
        Project = new ProjectApi(ApiClient);
        System = new SystemApi(ApiClient);
        Obsolete = new ObsoleteApi(ApiClient);
    }

    /// <summary>Low level transport.</summary>
    public ApiClient ApiClient { get; }

    /// <summary>
    /// Ця група методів пов'язана з надсиланням email.
    /// </summary>
    public EmailApi Email { get; }

    /// <summary>
    /// Методи перевірки email-адрес. Перевірка адреси електронної пошти надає докладну інформацію про<br/>
    /// працездатність email. Це дозволить виключити з надсилання неіснуючі email, знизити показник скарг,<br/>
    /// очистити список підписників і врешті-решт покращити вашу репутацію як відправника. Наприклад, ви<br/>
    /// можете інтегрувати процес перевірки у форму реєстрації вашого сайту і вже на етапі введення адреси<br/>
    /// повідомляти користувачам, що адреса, яку вони ввели у форму, недійсна.
    /// </summary>
    public EmailValidationApi EmailValidation { get; }

    /// <summary>
    /// Керування email-адресами, підтвердженими для надсилання із sandbox-домену. Методи доступні лише з<br/>
    /// API-ключем основного облікового запису.
    /// </summary>
    public ConfirmedEmailApi ConfirmedEmail { get; }

    /// <summary>
    /// Ви можете зберегти шаблон на боці AllyMailer і потім посилатися на нього за ідентифікатором. Це не<br/>
    /// обов'язково, але вкрай рекомендовано у разі повторюваних email-повідомлень або за великих обсягів<br/>
    /// надсилання. AllyMailer підтримує два різні синтаксиси для шаблонів, докладніше читайте<br/>
    /// [тут](https://docs.allymailer.com.ua/en/template-engines).
    /// </summary>
    public TemplateApi Template { get; }

    /// <summary>
    /// AllyMailer дозволяє вам отримувати сповіщення про зміну статусу доставки листа або спам-блокування.<br/>
    /// Ви надаєте URL, і в разі виникнення події, що вас цікавить, ми сповіщаємо вас, передаючи на цей URL<br/>
    /// JSON-дані про подію.
    /// </summary>
    public WebhookApi Webhook { get; }

    /// <summary>
    /// Методи для керування адресами, надсилання на які заблоковано з різних причин — відписка,<br/>
    /// недоступність, скарги.
    /// </summary>
    public SuppressionApi Suppression { get; }

    /// <summary>
    /// Методи для налаштування доменів надсилання та перевірки їх статусів. Корисні, якщо вам недостатньо<br/>
    /// додавання доменів через вебінтерфейс AllyMailer і, наприклад, хочеться автоматизувати додавання<br/>
    /// доменів для ваших власних клієнтів.
    /// </summary>
    public DomainApi Domain { get; }

    /// <summary>
    /// Методи для отримання списку подій, що сталися, за певний період. Дані зберігаються до 45 днів,<br/>
    /// залежно від вашого тарифу. Це означає, що можна запросити події, починаючи з 00:00:00 45-го дня тому<br/>
    /// і до поточного моменту часу. Для індивідуальних тарифів з великими обсягами за замовчуванням події<br/>
    /// доступні лише за останні 7 днів.
    /// </summary>
    public EventDumpApi EventDump { get; }

    /// <summary>
    /// Методи керування користувацькими мітками. За допомогою міток ви можете класифікувати листи за<br/>
    /// потрібними критеріями. Відокремити маркетингові листи від транзакційних або листи з підтвердженням<br/>
    /// реєстрації від сповіщень про замовлення. AllyMailer дозволяє позначати мітками кожне вихідне<br/>
    /// повідомлення, а потім сегментувати й аналізувати статистику на основі зазначених під час надсилання<br/>
    /// міток.
    /// </summary>
    public TagApi Tag { get; }

    /// <summary>
    /// Проєкти дозволяють об'єднати кілька незалежних доменів із webhook, шаблонами та списками відписки.<br/>
    /// Це дуже корисно, якщо у вас є власні клієнти і ви керуєте їхніми даними з одного облікового запису<br/>
    /// AllyMailer. Кожен проєкт отримує власний ключ API, а виклики методів API з цим ключем обробляють<br/>
    /// лише дані цього проєкту. Таким чином, ви можете призначити окремий проєкт для кожного користувача і<br/>
    /// бути впевненими, що вони не конфліктуватимуть: наприклад, відписка від одного проєкту не вплине на<br/>
    /// інший.
    /// </summary>
    public ProjectApi Project { get; }

    /// <summary>
    /// Системні методи.
    /// </summary>
    public SystemApi System { get; }

    /// <summary>
    /// Застарілі методи роботи з відписками. Ми рекомендуємо користуватися замість них гнучкішими методами<br/>
    /// [suppression](https://docs.allymailer.com.ua/ua/web-api-ref#suppression). Зверніть увагу, що знову<br/>
    /// підписати відписаного адресата через WebAPI не можна. Можна лише надіслати йому email із посиланням<br/>
    /// повторної підписки за допомогою методу email/subscribe.
    /// </summary>
    public ObsoleteApi Obsolete { get; }
}
