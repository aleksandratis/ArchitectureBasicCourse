# Диаграмма ограниченных контекстов
<!-- В качестве контекста может выступать:
Логическое приложение (ИТ-система, Software System) - логический агрегированный программный компонент, автоматизирующий сервисы какой-либо компетенции(-ий) (capability) в данном продукте.
Packaged Business Capability (PBC) - особый тип логического программного компонента (ИТ-системы), автоматизирующий сервисы отдельной компетенции(-ий) (capability) для многократного использования при создании других продуктов.
Подробнее: https://confluence.mts.ru/pages/viewpage.action?pageId=518200648
-->
```plantuml
@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

AddElementTag("context", $bgColor="#5588FF", $legendText="context")

Person(guest, "Гость", "Пользователь, пришедший посмотреть конференцию бесплатно.")
Person(customer, "Клиент", "Пользователь, купивший платный билет.")
Person(speaker, "Докладчик", "Пользователь, выступающий с докладом.")

Container(front, "Контекст базового фронтенда", "", "Web-портал для доступа к стандартным системам конференции.", $tags="context")
Container(accounts, "Контекст управления пользователями", "", "Регистрация, авторизация и аутентификация пользователей.", $tags="context")
Container(messages, "Контекст текстовых сообщений", "", "Отправка сообщений в чат и просмотр чата.", $tags="context")
Container(schedule, "Контекст календаря", "", "Расписание докладов.", $tags="context")

Container(broadcast, "Контекст трансляции конференции", "", "Трансляция докладов конференции.", $tags="context")
Container(storage, "Контекст хранения докладов", "", "Хранение видеозаписей докладов конференции.", $tags="context")

Rel(guest, front, "")
Rel(customer, front, "")
Rel(speaker, front, "")

Rel(front, accounts, "")
Rel(front, broadcast, "")
Rel(front, messages, "")
Rel(front, schedule, "")

Rel(customer, storage, "")

Rel(speaker, broadcast, "")

Rel(broadcast, storage, "")

SHOW_LEGEND()

@enduml
```
