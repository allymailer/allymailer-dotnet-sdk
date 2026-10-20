using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AllyMailer.Api.EventDump.Model;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.EventDump;

/// <summary>
/// Методи для отримання списку подій, що сталися, за певний період. Дані зберігаються до 45 днів,<br/>
/// залежно від вашого тарифу. Це означає, що можна запросити події, починаючи з 00:00:00 45-го дня тому<br/>
/// і до поточного моменту часу. Для індивідуальних тарифів з великими обсягами за замовчуванням події<br/>
/// доступні лише за останні 7 днів.
/// </summary>
public class EventDumpApi
{
    private readonly ApiClient _client;

    public EventDumpApi(ApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Асинхронний метод, який ініціює підготовку для завантаження файлів дампа у форматі CSV. Максимальна<br/>
    /// кількість збережених/створюваних дампів обмежена десятьма. У разі досягнення ліміту для запиту на<br/>
    /// підготовку нового дампа потрібно видалити старі. Час зберігання сформованого дампа — 8 годин, після<br/>
    /// чого він автоматично видаляється. Якщо період запиту даних більший за добу або подій багато, буде<br/>
    /// сформовано декілька файлів, у кожному з яких буде не більше ніж 100 000 подій. Файл містить колонки<br/>
    /// з такою інформацією — 'event_time', 'job_id', 'from_email', 'email', 'status', 'delivery_status',<br/>
    /// 'metadata', 'destination_response', 'user_agent', 'url', 'ip', 'tags' і 'project_id' (якщо у<br/>
    /// користувача увімкнено проєкти). Ви можете відфільтрувати поля та змінити їх порядок, а також додати<br/>
    /// поле 'subject' за допомогою параметра 'dump_fileds'.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-create
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EventDumpCreateResponse> CreateAsync(EventDumpCreateRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EventDumpCreateResponse>("/event-dump/create.json", request, cancellationToken);
    }

    /// <summary>
    /// Повертає властивості дампа за його ідентифікатором.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-get
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EventDumpGetResponse> GetAsync(EventDumpGetRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EventDumpGetResponse>("/event-dump/get.json", request, cancellationToken);
    }

    /// <summary>
    /// Метод без параметрів, повертає список усіх дампів подій.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-list
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<EventDumpListResponse> ListAsync(CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<EventDumpListResponse>("/event-dump/list.json", null, cancellationToken);
    }

    /// <summary>
    /// Видаляє дамп із черги та/або зі сховища.
    /// <para/>
    /// Документація: https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-delete
    /// </summary>
    /// <exception cref="ApiException">The API returned an error.</exception>
    public Task<SuccessResponse> DeleteAsync(EventDumpDeleteRequest request, CancellationToken cancellationToken = default)
    {
        return _client.CallAsync<SuccessResponse>("/event-dump/delete.json", request, cancellationToken);
    }
}
