# Template

[До списку розділів](../README.md)

Ви можете зберегти шаблон на боці AllyMailer і потім посилатися на нього за ідентифікатором. Це не обов'язково, але вкрай рекомендовано у разі повторюваних email-повідомлень або за великих обсягів надсилання. AllyMailer підтримує два різні синтаксиси для шаблонів, докладніше читайте [тут](https://docs.allymailer.com.ua/ua/template-engines).

`AllyMailer.Api.Template.TemplateApi`, `client.Template`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`SetAsync`](#setasync) | `POST /template/set.json` | Створити або оновити шаблон. |
| [`GetAsync`](#getasync) | `POST /template/get.json` | Повертає властивості шаблону за його ідентифікатором. |
| [`ListAsync`](#listasync) | `POST /template/list.json` | Повертає повний список або частину списку всіх шаблонів. |
| [`DeleteAsync`](#deleteasync) | `POST /template/delete.json` | Видаляє шаблон за ідентифікатором. |

## SetAsync

`POST /template/set.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#template-set)

Створити або оновити шаблон.

Обмеження:
* Максимальний розмір запиту — 10 MB.
* Кількість шаблонів обмежено 10000.

### Запит: `TemplateSetRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Template` | `TemplateObject` | так | Об'єкт, що містить властивості шаблону. |

### Відповідь: `TemplateSetResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Template` | `TemplateObject` | Об'єкт, що містить властивості шаблону. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Template.Model;
using AllyMailer.Common.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Template.SetAsync(new TemplateSetRequest
{
    Template = new TemplateObject
    {
        Name = "name",
        EditorType = EditorType.Html,
        TemplateEngine = TemplateEngine.Velocity,
        Subject = "AllyMailer test email ",
        FromEmail = "user@example.com",
        FromName = "John Smith",
        ReplyTo = "email@example.com",
        ReplyToName = "John Smith",
    },
});
Console.WriteLine(response.Template);
```

## GetAsync

`POST /template/get.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#template-get)

Повертає властивості шаблону за його ідентифікатором.

### Запит: `TemplateGetRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Id` | `string` | так | Ідентифікатор шаблону. |

### Відповідь: `TemplateGetResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Template` | `TemplateObject` | Об'єкт, що містить властивості шаблону. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Template.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Template.GetAsync(new TemplateGetRequest
{
    Id = "00000000-0000-0000-0000-000000000000",
});
Console.WriteLine(response.Template);
```

## ListAsync

`POST /template/list.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#template-list)

Повертає повний список або частину списку всіх шаблонів.

Ви можете переглядати великий список частинами, використовуючи параметри "offset" і "limit". Якщо повернуто менше ніж "limit" шаблонів — значить, ви досягли кінця списку.

### Запит: `TemplateListRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Limit` | `int` |  | Максимальна кількість шаблонів, що повертаються, за замовчуванням 50. |
| `Offset` | `int` |  | Індекс першого шаблону зі списку, що повертається, рахуючи з нуля. |

### Відповідь: `TemplateListResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Templates` | `List<TemplateListObject>` | Масив об'єктів-шаблонів. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Template.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Template.ListAsync(new TemplateListRequest());
Console.WriteLine(response.Templates);
```

## DeleteAsync

`POST /template/delete.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#template-delete)

Видаляє шаблон за ідентифікатором.

### Запит: `TemplateDeleteRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Id` | `string` | так | Ідентифікатор шаблону. |

### Відповідь: `SuccessResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Template.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Template.DeleteAsync(new TemplateDeleteRequest
{
    Id = "00000000-0000-0000-0000-000000000000",
});
Console.WriteLine(response);
```
