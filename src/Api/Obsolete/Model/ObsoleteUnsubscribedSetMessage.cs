using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Api.Obsolete.Model;

/// <summary>
/// Подробиці відписки:
/// <para/>
/// - "unsubscribed" якщо email було щойно відписано,<br/>
/// - "was inactive" якщо він уже був позначений як неактивний і немає сенсу відписувати,<br/>
/// - "was unsubscribed" якщо email уже було відписано раніше,<br/>
/// - "added and unsubscribed" якщо для цього email не зберігалося жодного більш раннього статусу, тому його було додано й одразу ж відписано.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<ObsoleteUnsubscribedSetMessage>))]
public enum ObsoleteUnsubscribedSetMessage
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("unsubscribed")]
    Unsubscribed,

    [EnumValue("was inactive")]
    WasInactive,

    [EnumValue("was unsubscribed")]
    WasUnsubscribed,

    [EnumValue("added and unsubscribed")]
    AddedAndUnsubscribed,
}
