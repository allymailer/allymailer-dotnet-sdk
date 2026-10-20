using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EventDump.Model;

/// <summary>
/// EventDumpGetResponse
/// </summary>
public class EventDumpGetResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Об'єкт із властивостями дампа подій.
    /// </summary>
    [JsonPropertyName("event_dump")]
    public EventDumpObject? EventDump { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
