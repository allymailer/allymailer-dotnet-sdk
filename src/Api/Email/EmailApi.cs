using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Email.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Email;

/// <summary>
/// Ця група методів пов'язана з надсиланням email.
/// </summary>
public class EmailApi
{
    private readonly ApiClient _client;

    public EmailApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Метод для надсилання листів вашим підписникам.
    /// <para/>
    /// Під час надсилання ви можете використовувати підстановки змінних, шаблони, увімкнути відстеження<br/>
    /// прочитань або переходів за посиланнями та багато іншого.
    /// <para/>
    /// Обмеження:<br/>
    /// * максимальний розмір запиту — 10 MB<br/>
    /// * максимальна кількість отримувачів (recipients) — 500 email<br/>
    /// * у назві вкладень заборонено символ '/'
    /// <para/>
    /// Якщо під час надсилання листа у списку отримувачів будуть некоректно введені, неіснуючі, повторювані<br/>
    /// або відписані адреси, вони будуть повернуті у відповіді за допомогою параметра failed_emails, лист<br/>
    /// же буде надіслано на всі інші прийнятні адреси. Якщо всі адреси потраплять до failed_emails,<br/>
    /// надсилання здійснено не буде, буде повернуто помилку з API-кодом 204.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#email-send
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EmailSendResponse> SendAsync(EmailSendRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EmailSendResponse>("/email/send.json", request, cancellationToken);
    }

    /// <summary>
    /// Надіслати посилання на (пере)підписку.
    /// <para/>
    /// Метод дозволяє повторно підписати колишнього підписника, який відписався раніше, був заблокований<br/>
    /// через скарги або чия адреса недоступна з якоїсь іншої причини.
    /// <para/>
    /// Під час виклику надсилається лист у нейтральному дизайні з посиланням підписки. При переході за<br/>
    /// посиланням з email підписник перестає бути недоступним для користувача та/або перестає бути<br/>
    /// глобально відписаним.
    /// <para/>
    /// За замовчуванням дозволено лише одне надсилання листа повторної підписки на добу. Якщо вам потрібно<br/>
    /// більше — напишіть у техпідтримку.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#email-subscribe
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> SubscribeAsync(EmailSubscribeRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/email/subscribe.json", request, cancellationToken);
    }
}
