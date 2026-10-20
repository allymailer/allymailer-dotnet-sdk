using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// EventDumpGetRequest
/// </summary>
public class EventDumpGetRequest
{
    /// <summary>
    /// Ідентифікатор дампа, отриманий методом<br/>
    /// [event-dump/create](https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-create).
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
