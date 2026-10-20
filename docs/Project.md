# Project

[До списку розділів](../README.md)

Проєкти дозволяють об'єднати кілька незалежних доменів із webhook, шаблонами та списками відписки. Це дуже корисно, якщо у вас є власні клієнти і ви керуєте їхніми даними з одного облікового запису AllyMailer. Кожен проєкт отримує власний ключ API, а виклики методів API з цим ключем обробляють лише дані цього проєкту. Таким чином, ви можете призначити окремий проєкт для кожного користувача і бути впевненими, що вони не конфліктуватимуть: наприклад, відписка від одного проєкту не вплине на інший.

Методи проєктів за замовчуванням вимкнено через заходи захисту від спаму. Щоб їх увімкнути, вам необхідно звернутися до [служби підтримки](https://cp.allymailer.com/ua/support).

Зверніть увагу: за замовчуванням домени для надсилання підтверджуються та використовуються незалежно для основного облікового запису й кожного з проєктів. Але є можливість увімкнути для проєктів використання доменів, підтверджених в основному обліковому записі. Просто попросіть про це [техпідтримку](https://cp.allymailer.com/ua/support).

`AllyMailer.Api.Project.ProjectApi`, `client.Project`

| Метод | Ендпоінт | Опис |
|---|---|---|
| [`CreateAsync`](#createasync) | `POST /project/create.json` | Створити новий проєкт. |
| [`UpdateAsync`](#updateasync) | `POST /project/update.json` | Змінити властивості проєкту. |
| [`ListAsync`](#listasync) | `POST /project/list.json` | Повертає список проєктів. |
| [`DeleteAsync`](#deleteasync) | `POST /project/delete.json` | Видалити проєкт. |

## CreateAsync

`POST /project/create.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#project-create)

Створити новий проєкт.

### Запит: `ProjectCreateRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `Project` | `ProjectObject` | так | Об'єкт із властивостями проєкту. |

### Відповідь: `ProjectCreateResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `ProjectId` | `string` | Унікальний ідентифікатор проєкту, ASCII-рядок до 36 символів завдовжки. |
| `ProjectApiKey` | `string` | API-ключ проєкту. Ви можете використовувати цей ключ замість користувацького api_key у всіх методах, крім project-методів. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Project.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Project.CreateAsync(new ProjectCreateRequest
{
    Project = new ProjectObject
    {
        Name = "Project 1A",
        Country = "ES",
        BackendDomainId = 1234,
        UnsubscribePageId = 1,
    },
});
Console.WriteLine(response.ProjectId);
```

## UpdateAsync

`POST /project/update.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#project-update)

Змінити властивості проєкту.

### Запит: `ProjectUpdateRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `ProjectId` | `string` |  | Ідентифікатор проєкту, що оновлюється. Проєкт має бути ідентифіковано або цим полем project_id, або полем project_api_key. Ми рекомендуємо використовувати project_id, це безпечніше. |
| `ProjectApiKey` | `string` |  | Необов'язковий API-ключ проєкту, що оновлюється. Проєкт має бути ідентифіковано або полем project_id, або полем project_api_key. Ми рекомендуємо використовувати project_id, це безпечніше. |
| `Project` | `ProjectUpdateObject` | так | Об'єкт із властивостями проєкту. |

### Відповідь: `ProjectUpdateResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `ProjectApiKey` | `string` | API-ключ проєкту. Повертається лише в разі, якщо в параметрі запиту був project_api_key. Якщо ж проєкт було ідентифіковано через project_id, то не повертається. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Project.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Project.UpdateAsync(new ProjectUpdateRequest
{
    ProjectId = "1234567890",
    ProjectApiKey = "your api_key",
    Project = new ProjectUpdateObject
    {
        Name = "Project 1A",
        Country = "ES",
        BackendDomainId = 1234,
        UnsubscribePageId = 1,
    },
});
Console.WriteLine(response.ProjectApiKey);
```

## ListAsync

`POST /project/list.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#project-list)

Повертає список проєктів.

### Запит: `ProjectListRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `ProjectId` | `string` |  | Опціональна можливість указати project_id для включення до списку лише вказаного проєкту. |
| `ProjectApiKey` | `string` |  | Опціональна можливість указати project_api_key для включення до списку лише вказаного проєкту. |

### Відповідь: `ProjectListResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |
| `Projects` | `List<ProjectListObject>` | Масив об'єктів, де кожен об'єкт відповідає окремому проєкту. |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Project.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Project.ListAsync(new ProjectListRequest
{
    ProjectId = "1234567890",
    ProjectApiKey = "your api_key",
});
Console.WriteLine(response.Projects);
```

## DeleteAsync

`POST /project/delete.json` · [Документація](https://docs.allymailer.com.ua/ua/web-api-ref#project-delete)

Видалити проєкт.

Деякий час проєкт не видаляється остаточно, і його ще можна відновити — для цього необхідно звернутися в техпідтримку.

### Запит: `ProjectDeleteRequest`

| Поле | Тип | Обов'язкове | Опис |
|---|---|---|---|
| `ProjectId` | `string` |  | Ідентифікатор проєкту, що видаляється. Проєкт має бути ідентифіковано або цим полем project_id, або полем project_api_key. Ми рекомендуємо використовувати project_id, це безпечніше. |
| `ProjectApiKey` | `string` |  | Необов'язковий API-ключ проєкту, що видаляється. Проєкт має бути ідентифіковано або полем project_id, або полем project_api_key. Ми рекомендуємо використовувати project_id, це безпечніше. |

### Відповідь: `SuccessResponse`

| Поле | Тип | Опис |
|---|---|---|
| `Status` | `string` | Рядок "success". (`success`) |

### Приклад

```csharp
using AllyMailer;
using AllyMailer.Api.Project.Model;

var client = new AllyMailerClient("YOUR_API_KEY");

var response = await client.Project.DeleteAsync(new ProjectDeleteRequest
{
    ProjectId = "1234567890",
    ProjectApiKey = "your api_key",
});
Console.WriteLine(response);
```
