using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Template.Model;

/// <summary>
/// Об'єкт з усіма властивостями шаблону.
/// </summary>
public class TemplateSetRequest
{
    /// <summary>
    /// Об'єкт, що містить властивості шаблону.
    /// </summary>
    [JsonPropertyName("template")]
    public TemplateObject? Template { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
