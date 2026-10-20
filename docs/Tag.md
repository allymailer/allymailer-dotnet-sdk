# Tag

[До списку розділів](../README.md)

Методи керування користувацькими мітками. За допомогою міток ви можете класифікувати листи за потрібними критеріями. Відокремити маркетингові листи від транзакційних або листи з підтвердженням реєстрації від сповіщень про замовлення. AllyMailer дозволяє позначати мітками кожне вихідне повідомлення, а потім сегментувати й аналізувати статистику на основі зазначених під час надсилання міток.

`AllyMailer.Api.Tag.TagApi`, `client.Tag`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`ListAsync`](#listasync) | `POST /tag/list.json` | Метод без параметрів, повертає список усіх користувацьких міток. |
| [`DeleteAsync`](#deleteasync) | `POST /tag/delete.json` | Видаляє мітку. |

## ListAsync

`POST /tag/list.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#tag-list)

Метод без параметрів, повертає список усіх користувацьких міток.

### Запит

Метод не має параметрів.

### Відповідь: `TagListResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Tags` | `List<TagsResponseObject>` | Масив об'єктів з даними користувацьких міток. |

### Приклад

```csharp
using AllyMailer;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Tag.ListAsync();
Console.WriteLine(response.Tags);
```

## DeleteAsync

`POST /tag/delete.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#tag-delete)

Видаляє мітку.

### Запит: `TagDeleteRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `TagId` | `int` | так | Унікальний id мітки. |

### Відповідь: `SuccessResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Tag.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Tag.DeleteAsync(new TagDeleteRequest
{
    TagId = 54,
});
Console.WriteLine(response);
```
