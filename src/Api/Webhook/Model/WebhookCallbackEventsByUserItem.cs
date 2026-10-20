using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// WebhookCallbackEventsByUserItem
/// </summary>
public class WebhookCallbackEventsByUserItem
{
    /// <summary>
    /// Унікальний ідентифікатор користувача.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Ідентифікатор проєкту, присутній, якщо webhook було зареєстровано за допомогою API-ключа проєкту.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Назва проєкту, присутня, якщо webhook було зареєстровано за допомогою API-ключа проєкту.
    /// </summary>
    [JsonPropertyName("project_name")]
    public string? ProjectName { get; set; }

    /// <summary>
    /// Масив подій, про які сповіщає виклик webhook.
    /// </summary>
    [JsonPropertyName("events")]
    public List<WebhookCallbackEventsByUserItemEventsItem>? Events { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
