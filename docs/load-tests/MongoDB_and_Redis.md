# Нагрузочное тестирование MongoDB и MongoDB + Redis
## Описание тестирования
Тестирование проводилось на рабочей машине VDI.

Для WSL выделено 2 ядра и 4 гигабайта оперативной памяти.

Каждый тест длился 1 минуту.

Тестирование проводилось следующим образом:
* БД MongoDB, Redis и Web-сервисы запускались в контейнерах
* Утилита wrk запускалась в WSL

## Результаты
| Потоки | latency, ms | rps   | latency, ms (with Redis) | rps (with Redis) |
|-------:|------------:|------:|-------------------------:|-----------------:|
| 1      | 10.83       | 92.35 | 8.54                     | 138.33           |
| 10     | 36.11       | 28.90 | 15.53                    | 65.65            |
| 50     | 116.67      | 9.29  | 69.89                    | 14.26            |

## Сравнение с MariaDB
* БД MongoDB без Redis показала более высокие результаты чем MariaDB (примерно в 3 раза).
* Связка MongoDB + Redis показа более высокие результаты чем MariaDB + Redis (в 2 - 3 раза).

По идее, результаты с Redis должен были быть примерно одинаковыми у обоих БД.
Различия объясняются отличающейся конфигурацией тестового стенда.
В этот раз DockerDesktop был обновлён до версии 4.19, и все сервисы пережили тестирование в контейнерах.
Также временами "подтормаживающее" поведение VDI могло наложить свой отпечаток.