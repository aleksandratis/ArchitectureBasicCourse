# Компонентная архитектура
<!-- Состав и взаимосвязи компонентов системы между собой и внешними системами с указанием протоколов, ключевые технологии, используемые для реализации компонентов.
Диаграмма контейнеров C4 и текстовое описание. 
Подробнее: https://confluence.mts.ru/pages/viewpage.action?pageId=375783368
-->
## Контейнерная диаграмма

```plantuml
@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

AddElementTag("microService", $shape=EightSidedShape(), $bgColor="CornflowerBlue", $fontColor="white", $legendText="microservice")
AddElementTag("storage", $shape=RoundedBoxShape(), $bgColor="lightSkyBlue", $fontColor="white")

Person(guest, "Гость", "Пользователь, пришедший посмотреть конференцию бесплатно.")
Person(customer, "Клиент", "Пользователь, купивший платный билет.")
Person(speaker, "Докладчик", "Пользователь, выступающий с докладом.")

Container(clientFront, "Веб-приложение для посетителей", "html, JavaScript, React", "Фронтенд для клиентов и гостей")
Container(accounts, "Система управления пользователями", "C#/.NET", "Сервис аутентификации и авторизации", $tags = "microService")
Container(view, "Система видеотрансляции", "Golang, nginx", "Сервис трансляции онлайн-конференции", $tags = "microService")
Container(messages, "Система сообщений чата", "Golang, nginx", "Сервис чата", $tags = "microService")
Container(schedule, "Система календаря", "C#/.NET", "Сервис расписания докладов")

Container(speakerFront, "Веб-приложение для докладчиков", "html, JavaScript, React", "Фронтенд для докладчиков")
Container(broadcast, "Система записи конференции", "Golang, nginx", "Сервис записи докладов", $tags = "microService")

ContainerDb(storageSchedule, "БД календаря", "SQLite", "Хранение информации о расписании докладов", $tags = "storage")
ContainerDb(storageVideo, "БД докладов", "SQLite + видеофайлы", "Хранение информации о файлах видеозаписей докладов, и самих файлов", $tags = "storage")
ContainerDb(storageAccounts, "БД пользователей", "MongoDB", "Хранение информации о пользователях", $tags = "storage")
ContainerDb(storageMessages, "БД чата", "MongoDB", "Хранение сообщений чата", $tags = "storage")

Rel(guest, clientFront, "Просмотр трансляции доклада", "JSON, HTTPS")
Rel(customer, clientFront, "Просмотр трансляции доклада", "JSON, HTTPS")
Rel(speaker, speakerFront, "Вещание доклада", "JSON, HTTPS")

Rel(clientFront, accounts, "Аутентификация и авторизация пользователя", "JSON, HTTPS")
Rel(clientFront, messages, "Отправка сообщения в чат (сообщение):статус доставки", "JSON, HTTPS")
Rel(clientFront, schedule, "Просмотр расписания докладов:расписание", "JSON, HTTPS")
Rel(view, clientFront, "Поток видеотрансляции", "HTTPS")

Rel(customer, storageVideo, "Просмотр прошедших докладов(доклад):поток", "SQL, FileSystem")
Rel(schedule, storageSchedule, "Чтение расписания докладов:расписание", "SQL")
Rel(accounts, storageAccounts, "Запрос авторизации(аккаунт):статус", "SQL")
Rel(messages, storageMessages, "Сохранение сообщения(сообщение)", "SQL")

Rel(speakerFront, broadcast, "Передача видеопотока(поток)", "HTTPS")

Rel(broadcast, storageVideo, "Сохранение видеопотока(поток)", "SQL, FileSystem")
Rel(broadcast, view, "Передача видеопотока(поток)", "HTTPS")

SHOW_LEGEND()
@enduml
```

## Список компонентов
| Компонент                           | Роль/назначение                                           |
|:------------------------------------|:----------------------------------------------------------|
| *Веб-приложение для посетителей*    | *Интерфейс для взаимодействия с сервисами портала*        |
| *Веб-приложение для докладчиков*    | *Интерфейс для взаимодействия с сервисами портала*        |
| *Система управления пользователями* | *Регистрация, аутентификация и авторизация пользователей* |
| *Система сообщений чата*            | *Доставка сообщений в чат конференции*                    |
| *Система календаря*                 | *Предоставление информации о расписании докладов*         |
| *Система видеотрансляции*           | *Сервис для трансляции видеопотока*                       |
| *Система записи конференции*        | *Запись видеопотока доклада*                              |
| *БД пользователей*                  | *Хранение данных о пользователях*                         |
| *БД чата*                           | *Хранение истории чата*                                   |
| *БД календаря*                      | *Хранение информации о расписании докладов*               |
| *БД докладов*                       | *Хранение данных о видеофайлах докладов*                  |