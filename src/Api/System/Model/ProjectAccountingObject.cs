using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;
using AllyMailer.Common.Model;

namespace AllyMailer.Api.System.Model;

/// <summary>
/// Об'єкт із властивостями лічильника листів. Присутній лише якщо API-ключ, використаний для запиту,<br/>
/// був API-ключем проєкту.
/// </summary>
public class ProjectAccountingObject
{
    /// <summary>
    /// Значення лічильника листів для проєкту, збільшується під час надсилання кожного листа.
    /// </summary>
    [JsonPropertyName("email_counter")]
    public int? EmailCounter { get; set; }

    /// <summary>
    /// Дозволяє обмежити кількість надісланих проєктом листів. У разі досягнення цього ліміту надсилання<br/>
    /// припиняється. Значення 0 (за замовчуванням) означає відсутність ліміту.
    /// </summary>
    [JsonPropertyName("email_counter_limit")]
    public int? EmailCounterLimit { get; set; }

    /// <summary>
    /// 'default' - лічильник автоматично скидається в нуль на початку кожного облікового періоду,<br/>
    /// 'permanent' - лічильник ніколи не скидається автоматично.<br/>
    /// За замовчуванням: `default`.
    /// </summary>
    [JsonPropertyName("email_counter_mode")]
    public EmailCounterMode? EmailCounterMode { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
