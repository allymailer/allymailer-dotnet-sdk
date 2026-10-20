using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// EventDumpCreateRequest
/// </summary>
public class EventDumpCreateRequest
{
    /// <summary>
    /// Дата й час у форматі РРРР-ММ-ДД гг:хх:сс, починаючи з якої необхідно отримати події. Дані<br/>
    /// зберігаються до 45 днів, залежно від вашого тарифу.
    /// </summary>
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    /// <summary>
    /// Дата й час у форматі РРРР-ММ-ДД гг:хх:сс, до якої (не включно) запитуються події.
    /// </summary>
    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    /// <summary>
    /// Максимальна кількість подій, що повертаються, за замовчуванням 50. Якщо це значення перевищує 100<br/>
    /// 000, буде створено декілька файлів, кожен з яких містить максимум 100 000 подій.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Якщо у користувача увімкнено проєкти, all_projects=true дозволить отримати дані всіх проєктів.<br/>
    /// За замовчуванням: `False`.
    /// </summary>
    [JsonPropertyName("all_projects")]
    public bool? AllProjects { get; set; }

    /// <summary>
    /// Об'єкт із властивостями фільтра дампа подій.
    /// </summary>
    [JsonPropertyName("filter")]
    public FilterObject? Filter { get; set; }

    /// <summary>
    /// Масив із рядків з переліком назв стовпців, які необхідно отримати у вивантаженні (у порядку<br/>
    /// переліку). Якщо 'dump_fields' відсутній або порожній — застосовується набір і порядок полів за<br/>
    /// замовчуванням. Припустимі значення (у порядку за замовчуванням): 'project_id', 'event_time',<br/>
    /// 'job_id', 'from_email', 'email', 'status', 'delivery_status', 'metadata', 'destination_response',<br/>
    /// 'user_agent', 'url', 'ip', 'tags'. З метою більшої конфіденційності поле 'subject' не входить до<br/>
    /// набору полів за замовчуванням, але ви можете додати його, вказавши в параметрі 'dump_fields'.
    /// </summary>
    [JsonPropertyName("dump_fields")]
    public List<string>? DumpFields { get; set; }

    /// <summary>
    /// Дозволяє отримати агреговану статистику за вказаний період. Може набувати одного з двох значень:
    /// <para/>
    /// - __day_status__ - отримати агреговану статистику; за такого значення параметр filter має бути відсутнім або порожнім об'єктом (фільтри неприпустимі);<br/>
    /// - __порожній рядок__ - значення за замовчуванням, еквівалентне відсутності параметра aggregate, і метод event_dump/create у цьому разі працює як раніше.
    /// </summary>
    [JsonPropertyName("aggregate")]
    public string? Aggregate { get; set; }

    /// <summary>
    /// Роздільник полів, за замовчуванням ','. Припустимі значення ',' і ';'.
    /// </summary>
    [JsonPropertyName("delimiter")]
    public string? Delimiter { get; set; }

    /// <summary>
    /// Формат отримуваного файлу “csv” (за замовчуванням) або “csv_gzip”.<br/>
    /// За замовчуванням: `csv`.
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
