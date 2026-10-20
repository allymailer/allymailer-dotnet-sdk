using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// Лише домени зі статусом "confirmed" запису про володіння доменом можуть використовуватися для<br/>
/// надсилання листів.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<VerificationRecordStatus>))]
public enum VerificationRecordStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("confirmed")]
    Confirmed,

    [EnumValue("unconfirmed")]
    Unconfirmed,
}
