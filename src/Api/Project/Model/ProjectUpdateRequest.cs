using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Project.Model;

/// <summary>
/// ProjectUpdateRequest
/// </summary>
public class ProjectUpdateRequest
{
    /// <summary>
    /// Ідентифікатор проєкту, що оновлюється. Проєкт має бути ідентифіковано або цим полем project_id, або<br/>
    /// полем project_api_key. Ми рекомендуємо використовувати project_id, це безпечніше.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Необов'язковий API-ключ проєкту, що оновлюється. Проєкт має бути ідентифіковано або полем<br/>
    /// project_id, або полем project_api_key. Ми рекомендуємо використовувати project_id, це безпечніше.
    /// </summary>
    [JsonPropertyName("project_api_key")]
    public string? ProjectApiKey { get; set; }

    /// <summary>
    /// Об'єкт із властивостями проєкту.
    /// </summary>
    [JsonPropertyName("project")]
    public ProjectUpdateObject? Project { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
