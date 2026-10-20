using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.ConfirmedEmail.Model;

/// <summary>
/// ConfirmedEmail
/// </summary>
public class ConfirmedEmail
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Confirmation status of the email address.
    /// </summary>
    [JsonPropertyName("status")]
    public VerificationRecordStatus? Status { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
