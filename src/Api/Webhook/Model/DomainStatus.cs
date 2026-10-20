using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Це подія блокування чи розблокування. Властивість присутня лише якщо<br/>
/// event_name="transactional_spam_block".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<DomainStatus>))]
public enum DomainStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("blocked")]
    Blocked,

    [EnumValue("unblocked")]
    Unblocked,
}
