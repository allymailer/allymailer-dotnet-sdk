using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// Об'єкт із властивостями дампа подій.
/// </summary>
public class EventDumpObject
{
    /// <summary>
    /// Ідентифікатор дампа, отриманий методом<br/>
    /// [event-dump/create](https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-create).
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>
    /// Статус задачі. Одне з чотирьох значень:
    /// <para/>
    /// - __queued__ - очікує в черзі, обробка ще не починалася;<br/>
    /// - __in_process__ - триває обробка;<br/>
    /// - __ready__ - обробку завершено, файли доступні для завантаження;<br/>
    /// - __failed__ - обробку перервано з помилкою.
    /// </summary>
    [JsonPropertyName("dump_status")]
    public string? DumpStatus { get; set; }

    /// <summary>
    /// Масив об'єктів, кожен з яких позначає готовий до завантаження файл. Навіть якщо<br/>
    /// "dump_status"="in_process", уже можна завантажувати ті файли, які вказані в "files". У разі<br/>
    /// відсутності подій за заданими параметрами буде повернуто порожній масив.
    /// </summary>
    [JsonPropertyName("files")]
    public List<EventDumpFilesObject>? Files { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
