using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Suppression.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Suppression;

/// <summary>
/// Методи для керування адресами, надсилання на які заблоковано з різних причин — відписка,<br/>
/// недоступність, скарги.
/// </summary>
public class SuppressionApi
{
    private readonly ApiClient _client;

    public SuppressionApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Додає email-адресу до списку недоступних. Ви завжди зможете пізніше видалити цю адресу зі списку<br/>
    /// недоступних методом<br/>
    /// [suppression/delete](https://docs.allymailer.com.ua/ua/web-api-ref#suppression-delete).
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#suppression-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> SetAsync(SuppressionSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/suppression/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає причину й дату блокування email-адреси.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#suppression-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuppressionGetResponse> GetAsync(SuppressionGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuppressionGetResponse>("/suppression/get.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає список недоступних, починаючи з указаної дати.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#suppression-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuppressionListResponse> ListAsync(SuppressionListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuppressionListResponse>("/suppression/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Видаляє email-адресу зі списку недоступних (лише записи з is_deletable=true). Якщо жодного запису<br/>
    /// для email не знайдено, повертається помилка API 3003. Якщо записи про недоступність знайдено, але<br/>
    /// жодну з них видалити не можна, повертається помилка API 3004. Якщо перевищено ліміт видалень на<br/>
    /// добу, повертається помилка API 906.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#suppression-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(SuppressionDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/suppression/delete.json", request, cancellationToken);
    }
}
