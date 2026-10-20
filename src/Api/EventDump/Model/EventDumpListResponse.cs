using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// EventDumpListResponse
/// </summary>
public class EventDumpListResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Масив об'єктів, що описують дампи подій. Кожен об'єкт містить ті самі властивості, що й об'єкт<br/>
    /// 'event_dump', який повертає метод<br/>
    /// [event-dump/get](https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-get).
    /// </summary>
    [JsonPropertyName("event_dumps")]
    public List<EventDumpObject>? EventDumps { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
