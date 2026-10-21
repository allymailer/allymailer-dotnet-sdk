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

    /// <summary>An HTML body, with an optional plain-text part.</summary>
    /// <param name="html">The HTML part.</param>
    /// <param name="plaintext">The plain-text part.</param>
    public static BodyObject FromHtml(string html, string? plaintext = null)
    {
        return new BodyObject { Html = html, Plaintext = plaintext };
    }

    /// <summary>A plain-text body.</summary>
    /// <param name="plaintext">The text.</param>
    public static BodyObject FromText(string plaintext)
    {
        return new BodyObject { Plaintext = plaintext };
    }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
