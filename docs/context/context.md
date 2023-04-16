# Контекст решения
<!-- Окружение системы (роли, участники, внешние системы) и связи системы с ним. Диаграмма контекста C4 и текстовое описание. 
Подробнее: https://confluence.mts.ru/pages/viewpage.action?pageId=375783261
-->
```plantuml
@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

LAYOUT_WITH_LEGEND()

Person(guest, "Гость", "Пользователь, пришедший посмотреть конференцию бесплатно.")
Person(customer, "Клиент", "Пользователь, купивший платный билет.")
Person(speaker, "Докладчик", "Пользователь, выступающий с докладом.")

System(base, "Базовая система", "Обеспечивает доступ к стандартным системам конференции.")
System_Ext(accounts, "Система управления пользователями", "Обеспечивает регистрацию, авторизацию и аутентификацию пользователей.")
System_Ext(view, "Система просмотра трансляции", "Позволяет участникам просматривать онлайн-трансляцию конференции.")
System_Ext(messages, "Система сообщений", "Позволяет писать сообщения в чат.")
System_Ext(schedule, "Система календаря", "Отвечает за расписание докладов.")

System(broadcast, "Система вещания конференции", "Обеспечивает вещание докладов конференции и сохранение записей.")
SystemDb(storage, "Система хранения докладов", "Обеспечивает хранение видеозаписей докладов конференции.")

Rel(guest, base, "Использует")
Rel(customer, base, "Использует")
Rel(speaker, base, "Использует")

Rel(base, accounts, "Использует")
Rel(base, view, "Использует")
Rel(base, messages, "Использует")
Rel(base, schedule, "Использует")

Rel(customer, storage, "Читает")

Rel(speaker, broadcast, "Использует")

Rel(broadcast, storage, "Записывает")
Rel(broadcast, view, "Передаёт")

SHOW_LEGEND()

@enduml
```
