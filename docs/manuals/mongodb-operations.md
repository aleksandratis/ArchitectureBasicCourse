# Основные операции с MongoDB

## Aggregation
Операция агрегации предназначена для выполнение сложных операций анализа данных.
Она обрабатывает множество документов и возвращает посчитанный результат.
Можно использовать эту операцию для:
* группировки значений из нескольких документов
* выполнение операций над сгруппированными данными для возвращения единичного результата

Например, следующая операция содержит две стадии, и возвращает итоговое количество пицц среднего размера,
сгруппированных по названию пиццы:
``` js
db.orders.aggregate( [
   // Stage 1: Filter pizza order documents by pizza size
   {
      $match: { size: "medium" }
   },
   // Stage 2: Group remaining documents by pizza name and calculate total quantity
   {
      $group: { _id: "$name", totalQuantity: { $sum: "$quantity" } }
   }
] )
```

## Text search
**Text search** предназначен для полнотекстового поиска по строкам полей.
Для выполнения этого поиска MongoDB использует текстовый индекс и оператор `$text`.
Пример создания текстового индекса для полей **name** и **description**:
``` js
db.stores.createIndex( { name: "text", description: "text" } )
```

Пример использвания поиска всех складов, содержащих любые товары из списка:
``` js
db.stores.find( { $text: { $search: "apple banana cherry" } } )
```

## MapReduce
**MapReduce** предназначен для обработки больших объемов данных путём агрегации их в промежуточный результат.
Пример:
``` js
db.orders.mapReduce(
    function () { emit (this.cust_id, this.amount); },      // map
    function (key, values) { return Array.sum(values) },    // reduce
    {
        query: { status: "A" },                             // query
        out: "order_totals"                                 // output
    }
)
```

Начиная с MongoDB 5.0 функция mapReduce считается устаревшей. Вместо неё рекомендуется использовать [Agregation](#aggregation)

## Geospatial queries
**Geospatial queries** служат для поиска по геопространственным данным.
Пример для поиска документов, содержащих координаты, удалённые от точки поиска не менее чем на 1000 и не более чем на 5000 метров:
``` js
db.places.find(
   {
     location:
       { $near:
          {
            $geometry: { type: "Point",  coordinates: [ -73.9667, 40.78 ] },
            $minDistance: 1000,
            $maxDistance: 5000
          }
       }
   }
)
```

## Transactions
Транзакции обеспечивают согласованность в сложных операциях в нескольких документах./
В MongoDB операции с одним документом являются атомарными.
При необходимости обеспечения согласованности данных при операциях над несколькими документами или массивами следует использовать транзакции.
Пример работы с транзакцией:
``` js
var session = db.getMongo().startSession()

session.startTransaction({"readConcern": { "level": "snapshot" }, "writeConcern": { "w": "majority" } })

var authors = session.getDatabase('literature').getCollection('authors')

authors.insertOne( {
    "first_name": "Virginie",
    "last_name": "Despentes",
    "title": "Vernon Subutex" }
)

session.commitTransaction()
```