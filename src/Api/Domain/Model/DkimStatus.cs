using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Domain.Model;

/// <summary>
/// Лише домени зі статусом DKIM-запису "active" можуть використовуватися для надсилання листів.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<DkimStatus>))]
public enum DkimStatus
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("active")]
    Active,

    [EnumValue("inactive")]
    Inactive,
}
