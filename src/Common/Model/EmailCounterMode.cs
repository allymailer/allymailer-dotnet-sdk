using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// Встановлює режим роботи лічильника листів. Дозволяє вибрати, як має поводитися лічильник:<br/>
/// автоматично скидатися чи залишатися постійним. Припустимі значення: default - лічильник автоматично<br/>
/// скидається в нуль на початку кожного облікового періоду, permanent - лічильник ніколи не скидається<br/>
/// автоматично. Скидання може бути виконане лише вручну користувачем, передавши значення 0 у полі<br/>
/// 'email_counter'. Якщо поле не передано в запиті або передано значення null, значення режиму не<br/>
/// змінюється.
/// </summary>
[JsonConverter(typeof(StringEnumConverter<EmailCounterMode>))]
public enum EmailCounterMode
{
    /// <summary>A value unknown to this SDK version.</summary>
    UnknownValue,

    [EnumValue("default")]
    Default,

    [EnumValue("permanent")]
    Permanent,
}
