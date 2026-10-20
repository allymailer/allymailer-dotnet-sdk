using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Template.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Template;

/// <summary>
/// Ви можете зберегти шаблон на боці AllyMailer і потім посилатися на нього за ідентифікатором. Це не<br/>
/// обов'язково, але вкрай рекомендовано у разі повторюваних email-повідомлень або за великих обсягів<br/>
/// надсилання. AllyMailer підтримує два різні синтаксиси для шаблонів, докладніше читайте<br/>
/// [тут](https://docs.allymailer.com.ua/en/template-engines).
/// </summary>
public class TemplateApi
{
    private readonly ApiClient _client;

    public TemplateApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Створити або оновити шаблон.
    /// <para/>
    /// Обмеження:<br/>
    /// * Максимальний розмір запиту — 10 MB.<br/>
    /// * Кількість шаблонів обмежено 10000.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#template-set
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TemplateSetResponse> SetAsync(TemplateSetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TemplateSetResponse>("/template/set.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає властивості шаблону за його ідентифікатором.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#template-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TemplateGetResponse> GetAsync(TemplateGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TemplateGetResponse>("/template/get.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає повний список або частину списку всіх шаблонів.
    /// <para/>
    /// Ви можете переглядати великий список частинами, використовуючи параметри "offset" і "limit". Якщо<br/>
    /// повернуто менше ніж "limit" шаблонів — значить, ви досягли кінця списку.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#template-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<TemplateListResponse> ListAsync(TemplateListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<TemplateListResponse>("/template/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Видаляє шаблон за ідентифікатором.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#template-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(TemplateDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/template/delete.json", request, cancellationToken);
    }
}
