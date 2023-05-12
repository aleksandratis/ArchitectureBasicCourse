# Нагрузочное тестирование
## Описание тестирования
Тестирование проводилось на рабочей машине VDI.

Для WSL выделено 2 ядра и 4 гигабайта оперативной памяти.

Если запускать всё в контейнерах, то во время тестирования в 10 и 50 потоков docker (Docker Desktop for Windows) вёл себя неадекватно - зависал, и ждал ручной перезагрузки службы.
Поэтому тестирование проводилось следующим образом:
* БД MariaDB и Redis остались в контейнерах
* Web-сервисы запускались на хосте
* Утилита wrk запускалась в WSL

## Результаты
| Потоки | latency, ms | rps   | latency, ms (with redis) | rps (with redis) |
|-------:|------------:|------:|-------------------------:|-----------------:|
| 1      | 27.66       | 37.61 | 23.82                    | 46.91            |
| 10     | 187.35      | 6.06  | 35.31                    | 29.31            |
| 50     | 296.48      | 3.93  | 307.47                   | 2.25             |