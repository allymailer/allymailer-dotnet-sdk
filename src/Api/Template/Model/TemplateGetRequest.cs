using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Template.Model;

/// <summary>
/// TemplateGetRequest
/// </summary>
public class TemplateGetRequest
{
    /// <summary>
    /// Ідентифікатор шаблону.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
