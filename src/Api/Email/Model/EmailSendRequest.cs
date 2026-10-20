using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Email.Model;

/// <summary>
/// EmailSendRequest
/// </summary>
public class EmailSendRequest
{
    /// <summary>
    /// Об'єкт, що містить усі властивості повідомлення, яке надсилається.
    /// </summary>
    [JsonPropertyName("message")]
    public MessageObject? Message { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
