using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Тип даних, що містяться в об'єкті event_data. Або "transactional_email_status", або<br/>
/// "transactional_spam_block".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EventName>))]
public enum EventName
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("transactional_email_status")]
    TransactionalEmailStatus,

    [EnumValue("transactional_spam_block")]
    TransactionalSpamBlock,
}
