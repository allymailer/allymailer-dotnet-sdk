using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Suppression.Model;

/// <summary>
/// SuppressionListResponse
/// </summary>
public class SuppressionListResponse
{
    /// <summary>
    /// Рядок "success".<br/>
    /// Допустимі значення: `success`.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Масив об'єктів з інформацією про недоступність.
    /// </summary>
    [JsonPropertyName("suppressions")]
    public List<SuppressionListObject>? Suppressions { get; set; }

    /// <summary>
    /// Параметр указує, з якої позиції починати вибірку. Має бути порожнім або відсутнім для отримання<br/>
    /// першої порції даних; для отримання другої та наступних порцій його необхідно задавати, підставляючи<br/>
    /// значення поля "cursor" з попередньої порції.
    /// </summary>
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
