using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllyMailer.Client;

namespace AllyMailer.Common.Model;

/// <summary>
/// InlineAttachmentsArrayItem
/// </summary>
public class InlineAttachmentsArrayItem
{
    /// <summary>
    /// Тип вкладення, див. [MIME](https://en.wikipedia.org/wiki/MIME). Якщо не впевнені, використовуйте<br/>
    /// "application/octet-stream".
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Ідентифікатор вкладення. Якщо, наприклад, "name"="IMAGECID", то для виведення картинки з цього<br/>
    /// вкладення в HTML-тексті листа потрібно вказати &lt;img src="cid:IMAGECID"&gt;.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Вміст файлу у [base64](https://en.wikipedia.org/wiki/Base64). Максимальний розмір файлу 7 Мб<br/>
    /// (9786710 байт у base64).
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>The object as JSON.</summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, GetType(), ApiClient.JsonOptions);
    }
}
