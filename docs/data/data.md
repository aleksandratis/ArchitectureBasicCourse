# Модель предметной области
<!-- Логическая модель, содержащая бизнес-сущности предметной области, атрибуты и связи между ними. 
Подробнее: https://confluence.mts.ru/pages/viewpage.action?pageId=375782602

Используется диаграмма классов UML. Документация: https://plantuml.com/class-diagram 
-->

```plantuml
@startuml
' Логическая модель данных в варианте UML Class Diagram (альтернатива ER-диаграмме).
namespace Accounts {
    class Accounts {
        accounts : Account[]
    }

    class Account {
        login : string
        password : string
        name : string
        role : Role
    }

    enum Role {
        administrator
        speaker
        customer
        guest
    }

    Accounts *-- "*" Account
    Account -- Role
}

namespace Reports {
    class Reports {
        reports : Report[]
    }

    class Report {
        speaker : Account
        topic : string
        description : string
        fileName : string
    }

    Reports *-- "*" Report
}

namespace Schedule {
    class Events {
        events : Event[]
    }

    class Event {
        dateBegin : datetime 
        dateEnd : datetime 
        report : Report
    }

    Events *-- "*" Event
}

namespace Chat {
    class Messages {
        messages : Message[]
    }

    class Message {
        date : datetime
        account : Account
        text : string
    }

    Messages *-- "*" Message
}

Event *-- "1" Report
Report *-- "1" Account
Message *-- "1" Account

@enduml
```