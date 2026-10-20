using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.EmailValidation.Model;

/// <summary>
/// EmailValidationSingleRequest
/// </summary>
public class EmailValidationSingleRequest
{
    /// <summary>
    /// Email-адреса, яку необхідно перевірити.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
