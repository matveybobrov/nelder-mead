# Реализация алгоритма Нелдера-Мида
## Демо
https://nelder-mead.onrender.com/  
Может потребоваться какое-то время, чтобы сервер запустился. Он отключается автоматически при неактивности
## Установка

Для начала необходимо установить [.NET 8](https://dotnet.microsoft.com/en-us/download) для запуска проекта.  
Также нужен [Visual Studio](https://visualstudio.microsoft.com/ru/downloads/). Если уже установлен, то заново устанавливать не нужно.

Далее перейдите в [Репозиторий проекта](https://github.com/matveybobrov/nelder-mead.git), нажмите на кнопку "fork" и склонируйте репозиторий к себе в аккаунт.  

Далее склонируйте репозиторий в любую папку на пк:
- Зайдите в свой форкнутый репозиторий, нажмите на кнопку "Code" и скопируйте https адрес
- Перейдите в нужную папку используя терминал (Win+R и введите cmd) и команду ```cd [путь]```, например ```cd C:/repos```.  
- Затем пропишите команду
```sh
git clone [скопированный http адрес]
```
Проект готов к работе!

**Важно!**
Чтобы получить актуальный код проекта, необходимо прописать в терминале ```git pull```, находясь в корне проекта. Эта команда подтянет все изменения из удалённого репозитория в ваш локальный.  
Это нужно на случай, если вы заметите, что в вашем локальном проекте отсутсвует какой-то код, который был добавлен в удалённый репозиторий.

## Запуск проекта
Откройте проект (server/sdk.sln) в Visual Studio

Для запуска сервера нужно либо:
- Нажать кнопку "запуск без отладки" на панели инструментов
- Использовать сочетание клавиш Ctrl+F5
Сервер перезапускается автоматически при редактировании (сохранении) файлов в папке server.  

Проект запустится и откроется терминал, его можно использовать для отладки (Console.WriteLine())
Также в браузере автоматически откроется UI. Если этого не произошло, его можно открыть вручную, запустив файл index.html из папки client

Чтобы остановить работу проекта, нужно либо:
- Нажать кнопку "стоп" на панели инструментов
- Нажать Ctrl+C в терминале

## Директории
| Директория | Описание |
| ------ | ------ |
| server/| Вся логика сервера |
| server/Program.cs| Точка входа в приложение, роутинг |
| server/src/NelderMead.cs | Основной код по работе с алгоритмом |
| server/src/Helpers.cs | Используемые в ходе алгоритма дополнительные классы |
| serverTests | Здесь будут тесты |
| client/ | Вся логика клиента (браузерное приложение) |


## Разработчику алгоритма
- Весь код будет в папке server/src
- Пока что работаем с функцией $x^2+xy+y^2-6x-9y$, её решение - точка (1;4). Затем рассмотрим вариант работы с пользовательскими функциями
- В конструктор класса передаётся массив начальных трёх точек. В будущем возможно также будет передаваться функция, нужно придумать, как это организовать
- Чтобы проверять результат работы кода, можно выводить данные в консоль. Она откроется автоматически при запуске проекта
- Важно учитывать, что тестировщику придётся тестировать код, так что нужно создавать как можно больше методов, а не писать всё в одном.
- Результат работы алгоритма должен возвращать объект следующего вида:
```javascript
{
  // Решение алгоритма
  Solution: {X: 0, Y: 0},
  // Список шагов (три координаты, соответствующие треугольнику в анимации)
  Steps: [
    [{X: 0, Y: 0}, {X: 1, Y: 1}, {X: 2, Y: 2}],
    [{X: 0, Y: 0}, {X: 1, Y: 1}, {X: 2, Y: 2}],
    [{X: 0, Y: 0}, {X: 1, Y: 1}, {X: 2, Y: 2}],
    // ...
  ]
  // Возможно ещё понадобится вычислить все минимальные точки функции
}
```

## Тестировщику
- Все тесты пишутся в папке serverTests в файле UnitTests (можно создавать дополнительные файлы, если нужно)
- Тесты запускаются либо во вкладке Тест - Запуск всех тестов, либо сочетанием клавиш Ctrl+R, затем нажать "a"
- По-хорошему нужно проверить каждый метод, созданный разработчиком, парой тестов

## Как отправлять изменения
После того, как вы внесли изменения в код, необходимо зафиксировать их в git:  
```
git add .
git commit -m "[осмысленное название коммита]"
git push
```
Затем создайте pull request на github

## Задачи
- [x] Реализовать алгоритм для конкретного случая: $f(x)=x^2+xy+y^2-6x-9y$
- [x] Написать тесты для дальнейшего расширения алгоритма
- [x] Написать UI для конкретного случая
- [ ] Расширить алгоритм для работы с пользовательскими функциями и начальными точками
- [ ] Добавить эту возможность в UI
- [ ] Протестировать расширенный алгоритм

**По всем вопросам пишите в вк**
