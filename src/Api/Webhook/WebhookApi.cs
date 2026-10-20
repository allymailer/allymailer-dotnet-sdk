using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Webhook.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Webhook;

/// <summary>
/// AllyMailer дозволяє вам отримувати сповіщення про зміну статусу доставки листа або спам-блокування.<br/>
/// Ви надаєте URL, і в разі виникнення події, що вас цікавить, ми сповіщаємо вас, передаючи на цей URL<br/>
/// JSON-дані про подію.
/// <para/>
/// Ви можете налаштувати формат виклику webhook (просто JSON або JSON, запакований у gzip), які саме<br/>
/// події вас цікавлять, чи дозволено групувати кілька подій в одному виклику, чи потрібні вам такі<br/>
/// подробиці, як SMTP-відповідь сервера або user agent отримувача тощо.
/// <para/>
/// Зверніть увагу, що ваш обробник має вміти приймати щонайменше 5 паралельних викликів, до 100 подій у<br/>
/// кожному (але ви можете налаштувати максимально дозволену кількість паралельних викликів, збільшивши<br/>
/// її до 100).
/// <para/>
/// Якщо URL, на який надсилається webhook, недоступний (немає відповіді HTTP 200 OK протягом 3 секунд),<br/>
/// спроби надсилання webhook на цей URL до отримання очікуваного 200 OK триватимуть протягом 24 годин з<br/>
/// інтервалом у 10 хвилин з додатковим параметром retry_count, значення якого збільшуватиметься на 1 з<br/>
/// кожним повторним надсиланням webhook. Якщо протягом 24 годин сталося щонайменше 10 подій і жоден із<br/>
/// багаторазових викликів webhook про ці події не був успішним, то ми вважаємо обробник непрацездатним<br/>
/// і автоматично встановлюємо його статус у неактивний та повідомляємо про це на email, [вказаний в<br/>
/// особистому кабінеті](https://cp.allymailer.com/ua/user/alerting/technical/).
/// <para/>
/// Формат webhook і структура кожної події описані<br/>
/// [тут](https://docs.allymailer.com.ua/ua/web-api-ref#callback-format).
/// </summary>
public class WebhookApi
{
    private readonly ApiClient _client;

    public WebhookApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Встановлює або змінює властивості [webhook](https://docs.allymailer.com.ua/ua/web-api-ref#webhook) —<br/>
    /// вашого обробника сповіщень про події. У момент виклику<br/>
    /// [webhook/set](https://docs.allymailer.com.ua/ua/web-api-ref#webhook-set) ми очікуємо, що до<br/>
    /// вказаного вами URL можна звернутися GET-запитом без параметрів і у відповідь надійде 200 OK.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#webhook-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<WebhookSetResponse> SetAsync(WebhookSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<WebhookSetResponse>("/webhook/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає властивості [webhook](https://docs.allymailer.com.ua/ua/web-api-ref#webhook)
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#webhook-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<WebhookGetResponse> GetAsync(WebhookGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<WebhookGetResponse>("/webhook/get.json", request, cancellationToken);
    }

    /// <summary>
    /// Повернути список усіх або кількох [webhook](https://docs.allymailer.com.ua/ua/web-api-ref#webhook)<br/>
    /// (обробників сповіщень про події) користувача або проєкту.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#webhook-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<WebhookListResponse> ListAsync(WebhookListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<WebhookListResponse>("/webhook/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Видалити webhook.
    /// <para/>
    /// Ви також можете тимчасово вимкнути webhook без його видалення, встановивши його поле status у<br/>
    /// "disabled" за допомогою методу<br/>
    /// [webhook/set](https://docs.allymailer.com.ua/ua/web-api-ref#webhook-set).
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#webhook-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(WebhookDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/webhook/delete.json", request, cancellationToken);
    }

    /// <summary>
    /// Опис формату webhook.
    /// </summary>
    /// <param name="body">Raw body of the incoming request.</param>
    public static WebhookCallback ParseCallback(string body)
    {
        return JsonSerializer.Deserialize<WebhookCallback>(body, ApiClient.JsonOptions)
            ?? throw new ApiException("Empty callback body", 0, null, body);
    }
}
