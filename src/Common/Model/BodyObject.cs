using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// Об'єкт, що містить у собі html-, plaintext- і amp-частини листа. Або html-, або plaintext-частина<br/>
/// має бути присутня обов'язково.
/// </summary>
public class BodyObject
{
    /// <summary>
    /// HTML-частина листа
    /// </summary>
    [JsonPropertyName("html")]
    public string? Html { get; set; }

    /// <summary>
    /// Plaintext-частина листа.
    /// </summary>
    [JsonPropertyName("plaintext")]
    public string? Plaintext { get; set; }

    /// <summary>
    /// Необов'язкова<br/>
    /// [AMP](http://amp.dev/documentation/guides-and-tutorials/learn/email_fundamentals/)-частина листа.
    /// </summary>
    [JsonPropertyName("amp")]
    public string? Amp { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
