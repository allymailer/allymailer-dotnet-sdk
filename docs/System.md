# System

[До списку розділів](../README.md)

Системні методи.

`AllyMailer.Api.System.SystemApi`, `client.System`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`PingAsync`](#pingasync) | `POST /system/ping.json` | Перевіряє валідність API-ключа та повертає ідентифікатор користувача. |
| [`InfoAsync`](#infoasync) | `POST /system/info.json` | Повертає інформацію про користувача або проєкт за API-ключем. |

## PingAsync

`POST /system/ping.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#system-ping)

Перевіряє валідність API-ключа та повертає ідентифікатор користувача.

### Запит

Метод не має параметрів.

### Відповідь: `SystemPingResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `UserId` | `int` | Унікальний ідентифікатор користувача. |

### Приклад

```csharp
using AllyMailer;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.System.PingAsync();
Console.WriteLine(response.UserId);
```

## InfoAsync

`POST /system/info.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#system-info)

Повертає інформацію про користувача або проєкт за API-ключем.

### Запит

Метод не має параметрів.

### Відповідь: `SystemInfoResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `UserId` | `int` | Унікальний ідентифікатор користувача. |
| `Email` | `string` | Email користувача. |
| `ProjectId` | `string` | Унікальний ідентифікатор проєкту, ASCII-рядок до 36 символів завдовжки. Присутній лише якщо API-ключ, використаний для запиту, був API-ключем проєкту. |
| `ProjectName` | `string` | Назва проєкту, унікальна в межах облікового запису користувача. Присутня лише якщо API-ключ, використаний для запиту, був API-ключем проєкту. |
| `ProjectAccounting` | `ProjectAccountingObject` | Об'єкт із властивостями лічильника листів. Присутній лише якщо API-ключ, використаний для запиту, був API-ключем проєкту. |
| `Accounting` | `SystemInfoResponseAccounting` | Об'єкт з властивостями періоду обліку. Присутній лише якщо API-ключ, використаний для запиту, був API-ключем користувача. |

### Приклад

```csharp
using AllyMailer;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.System.InfoAsync();
Console.WriteLine(response.UserId);
```
