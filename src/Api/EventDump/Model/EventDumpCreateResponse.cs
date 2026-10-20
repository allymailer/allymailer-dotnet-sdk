using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// EventDumpCreateResponse
/// </summary>
public class EventDumpCreateResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Ідентифікатор дампа. Використовується для виклику методу<br/>
    /// [event-dump/get](https://docs.allymailer.com.ua/ua/web-api-ref#event-dump-get).
    /// </summary>
    [JsonPropertyName("dump_id")]
    public string? DumpId { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
