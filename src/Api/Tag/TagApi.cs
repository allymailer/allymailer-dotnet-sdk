using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Tag.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Tag;

/// <summary>
/// Методи керування користувацькими мітками. За допомогою міток ви можете класифікувати листи за<br/>
/// потрібними критеріями. Відокремити маркетингові листи від транзакційних або листи з підтвердженням<br/>
/// реєстрації від сповіщень про замовлення. AllyMailer дозволяє позначати мітками кожне вихідне<br/>
/// повідомлення, а потім сегментувати й аналізувати статистику на основі зазначених під час надсилання<br/>
/// міток.
/// </summary>
public class TagApi
{
    private readonly ApiClient _client;

    public TagApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Метод без параметрів, повертає список усіх користувацьких міток.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#tag-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TagListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TagListResponse>("/tag/list.json", null, cancellationToken);
    }

    /// <summary>
    /// Видаляє мітку.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#tag-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(TagDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/tag/delete.json", request, cancellationToken);
    }
}
