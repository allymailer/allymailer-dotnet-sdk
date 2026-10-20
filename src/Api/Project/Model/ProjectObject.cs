using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Project.Model;

/// <summary>
/// Об'єкт із властивостями проєкту.
/// </summary>
public class ProjectObject
{
    /// <summary>
    /// Назва проєкту, унікальна в межах облікового запису користувача.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Двобуквений код країни за правилами ISO-3166 alpha-2. Якщо задано, то AllyMailer обробляє<br/>
    /// персональні дані проєкту відповідно до законів заданої країни — наприклад, відповідно до GDPR для<br/>
    /// європейських країн.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Чи дозволено надсилання листів для проєкту.<br/>
    /// За замовчуванням: `True`.
    /// </summary>
    [JsonPropertyName("send_enabled")]
    public bool? SendEnabled { get; set; }

    /// <summary>
    /// Якщо false, то AllyMailer додає в кінець кожного листа системне посилання відписки. Значення true<br/>
    /// вимикає автоматичне додавання системою посилання відписки в кінець листа й дозволяє використовувати<br/>
    /// власне посилання відписки або надсилати листи зовсім без посилання відписки. Докладніше див.<br/>
    /// [тут](https://docs.allymailer.com.ua/en/unsubscribe-link). Якщо ви не вказуєте значення<br/>
    /// custom_unsubscribe_url_enabled під час створення проєкту — то це значення копіюється з головного<br/>
    /// облікового запису (якщо в головному обліковому записі було встановлено примусове вимкнення посилання<br/>
    /// відписки — то й у проєкту посилання відписки буде вимкнено).
    /// </summary>
    [JsonPropertyName("custom_unsubscribe_url_enabled")]
    public bool? CustomUnsubscribeUrlEnabled { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор backend-домену, який визначає [домен посилань](/tracking-domains) або пул<br/>
    /// [виділених IP-адрес](/dedicated-ip) за замовчуванням. Якщо відсутній — обліковому запису/проєкту<br/>
    /// буде призначено дефолтний backend-домен за замовчуванням.
    /// </summary>
    [JsonPropertyName("backend_domain_id")]
    public int? BackendDomainId { get; set; }

    /// <summary>
    /// Унікальний ідентифікатор, який визначає сторінку відписки за замовчуванням. Якщо відсутній —<br/>
    /// обліковому запису/проєкту буде призначено системну сторінку відписки за замовчуванням.
    /// </summary>
    [JsonPropertyName("unsubscribe_page_id")]
    public int? UnsubscribePageId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
