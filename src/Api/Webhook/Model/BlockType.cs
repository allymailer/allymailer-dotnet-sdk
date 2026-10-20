using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Webhook.Model;

/// <summary>
/// Тип спам-блока — заблоковано або один відправляючий SMTP, або одразу багато. У разі блокування<br/>
/// одиночного SMTP із загального пулу AllyMailer пробуватиме ще декілька. Властивість присутня лише<br/>
/// якщо event_name="transactional_spam_block".
/// </summary>
[JsonConverter(typeof(StringEnumConverter<BlockType>))]
public enum BlockType
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("one_smtp")]
    OneSmtp,

    [EnumValue("all_smtp")]
    AllSmtp,
}
