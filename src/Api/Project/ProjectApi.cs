using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.Project.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.Project;

/// <summary>
/// Проєкти дозволяють об'єднати кілька незалежних доменів із webhook, шаблонами та списками відписки.<br/>
/// Це дуже корисно, якщо у вас є власні клієнти і ви керуєте їхніми даними з одного облікового запису<br/>
/// AllyMailer. Кожен проєкт отримує власний ключ API, а виклики методів API з цим ключем обробляють<br/>
/// лише дані цього проєкту. Таким чином, ви можете призначити окремий проєкт для кожного користувача і<br/>
/// бути впевненими, що вони не конфліктуватимуть: наприклад, відписка від одного проєкту не вплине на<br/>
/// інший.
/// <para/>
/// Методи проєктів за замовчуванням вимкнено через заходи захисту від спаму. Щоб їх увімкнути, вам<br/>
/// необхідно звернутися до [служби підтримки](https://cp.allymailer.com/ua/support).
/// <para/>
/// Зверніть увагу: за замовчуванням домени для надсилання підтверджуються та використовуються незалежно<br/>
/// для основного облікового запису й кожного з проєктів. Але є можливість увімкнути для проєктів<br/>
/// використання доменів, підтверджених в основному обліковому записі. Просто попросіть про це<br/>
/// [техпідтримку](https://cp.allymailer.com/ua/support).
/// </summary>
public class ProjectApi
{
    private readonly ApiClient _client;

    public ProjectApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Створити новий проєкт.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#project-create
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ProjectCreateResponse> CreateAsync(ProjectCreateRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ProjectCreateResponse>("/project/create.json", request, cancellationToken);
    }

    /// <summary>
    /// Змінити властивості проєкту.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#project-update
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ProjectUpdateResponse> UpdateAsync(ProjectUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ProjectUpdateResponse>("/project/update.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає список проєктів.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#project-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<ProjectListResponse> ListAsync(ProjectListRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<ProjectListResponse>("/project/list.json", request, cancellationToken);
    }

    /// <summary>
    /// Видалити проєкт.
    /// <para/>
    /// Деякий час проєкт не видаляється остаточно, і його ще можна відновити — для цього необхідно<br/>
    /// звернутися в техпідтримку.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#project-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(ProjectDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/project/delete.json", request, cancellationToken);
    }
}
