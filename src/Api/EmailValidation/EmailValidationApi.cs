using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.EmailValidation.Model;
using AllyMailer.Client;

namespace AllyMailer.Api.EmailValidation;

/// <summary>
/// Методи перевірки email-адрес. Перевірка адреси електронної пошти надає докладну інформацію про<br/>
/// працездатність email. Це дозволить виключити з надсилання неіснуючі email, знизити показник скарг,<br/>
/// очистити список підписників і врешті-решт покращити вашу репутацію як відправника. Наприклад, ви<br/>
/// можете інтегрувати процес перевірки у форму реєстрації вашого сайту і вже на етапі введення адреси<br/>
/// повідомляти користувачам, що адреса, яку вони ввели у форму, недійсна.
/// </summary>
public class EmailValidationApi
{
    private readonly ApiClient _client;

    public EmailValidationApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Допускається не більше ніж два одночасні запити; у разі перевищення буде повернуто помилку. Оскільки<br/>
    /// перевірка має бути швидкою, ми обмежуємо час, що витрачається на перевірку адреси електронної пошти.<br/>
    /// Тому, якщо ми зіткнемося з повільним поштовим сервером, час відповіді якого перевищує 5 секунд,<br/>
    /// результат перевірки може бути невідомим.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#email-validation-single
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EmailValidationSingleResponse> SingleAsync(EmailValidationSingleRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EmailValidationSingleResponse>("/email-validation/single.json", request, cancellationToken);
    }
}
