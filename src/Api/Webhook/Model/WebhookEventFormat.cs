using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Формат сповіщень. "json_post"(за замовчуванням) або "json_post_gzip".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<WebhookEventFormat>))]
public enum WebhookEventFormat
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("json_post")]
    JsonPost,

    [EnumValue("json_post_gzip")]
    JsonPostGzip,
}
