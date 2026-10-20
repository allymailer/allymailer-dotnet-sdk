using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Obsolete.Model;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete;

/// <summary>
/// Застарілі методи роботи з відписками. Ми рекомендуємо користуватися замість них гнучкішими методами<br/>
/// [suppression](https://docs.allymailer.com.ua/ua/web-api-ref#suppression). Зверніть увагу, що знову<br/>
/// підписати відписаного адресата через WebAPI не можна. Можна лише надіслати йому email із посиланням<br/>
/// повторної підписки за допомогою методу email/subscribe.
/// </summary>
public class ObsoleteApi
{
    private readonly ApiClient _client;

    public ObsoleteApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Внести email до списку відписаних.
    /// <para/>
    /// Після внесення email до списку відписаних усі майбутні спроби надіслати на нього повідомлення будуть<br/>
    /// відхилятися. Зауважте, що видалити email зі списку відписаних безпосередньо за допомогою API не<br/>
    /// можна. Але можна надіслати йому повідомлення з посиланням повторної підписки за допомогою методу<br/>
    /// [email/subscribe](https://docs.allymailer.com.ua/ua/web-api-ref#email-subscribe) або видалити його<br/>
    /// зі списку відписаних вручну на сторінці [Пошук email](https://cp.allymailer.com/ua/user/email) у<br/>
    /// панелі керування (але кількість таких видалень на день сильно обмежена).
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#unsubscribed-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ObsoleteUnsubscribedSetResponse> UnsubscribedSetAsync(ObsoleteUnsubscribedSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ObsoleteUnsubscribedSetResponse>("/unsubscribed/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Перевіряє, чи відписано email.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#unsubscribed-check
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ObsoleteUnsubscribedCheckResponse> UnsubscribedCheckAsync(ObsoleteUnsubscribedCheckRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ObsoleteUnsubscribedCheckResponse>("/unsubscribed/check.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає список адрес, відписаних починаючи з указаної дати.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#unsubscribed-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ObsoleteUnsubscribedListResponse> UnsubscribedListAsync(ObsoleteUnsubscribedListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ObsoleteUnsubscribedListResponse>("/unsubscribed/list.json", request, cancellationToken);
    }
}
