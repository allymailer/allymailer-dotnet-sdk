# ConfirmedEmail

[До списку розділів](../README.md)

Керування email-адресами, підтвердженими для надсилання із sandbox-домену. Методи доступні лише з API-ключем основного облікового запису.

`AllyMailer.Api.ConfirmedEmail.ConfirmedEmailApi`, `client.ConfirmedEmail`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`CreateAsync`](#createasync) | `POST /confirmed-email/create.json` | Adds an email address and sends a confirmation link. |
| [`ListAsync`](#listasync) | `POST /confirmed-email/list.json` | Lists confirmed and unconfirmed email addresses for the account. |
| [`DeleteAsync`](#deleteasync) | `POST /confirmed-email/delete.json` | Deletes an email address from the account's confirmed email list. |

## CreateAsync

`POST /confirmed-email/create.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#confirmed-email-create)

Adds an email address and sends a confirmation link.

### Запит: `ConfirmedEmailRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Email` | `string` | так | Email address to add or delete. |

### Відповідь: `ConfirmedEmailCreateResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `ConfirmedEmail` | `ConfirmedEmail` |  |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.ConfirmedEmail.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.ConfirmedEmail.CreateAsync(new ConfirmedEmailRequest
{
    Email = "dev@example.com",
});
Console.WriteLine(response.ConfirmedEmail);
```

## ListAsync

`POST /confirmed-email/list.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#confirmed-email-list)

Lists confirmed and unconfirmed email addresses for the account.

### Запит

Метод не має параметрів.

### Відповідь: `ConfirmedEmailListResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `ConfirmedEmails` | `List<ConfirmedEmail>` |  |

### Приклад

```csharp
using AllyMailer;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.ConfirmedEmail.ListAsync();
Console.WriteLine(response.ConfirmedEmails);
```

## DeleteAsync

`POST /confirmed-email/delete.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#confirmed-email-delete)

Deletes an email address from the account's confirmed email list.

### Запит: `ConfirmedEmailRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Email` | `string` | так | Email address to add or delete. |

### Відповідь: `ConfirmedEmailDeleteResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Message` | `string` |  |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.ConfirmedEmail.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.ConfirmedEmail.DeleteAsync(new ConfirmedEmailRequest
{
    Email = "dev@example.com",
});
Console.WriteLine(response.Message);
```
