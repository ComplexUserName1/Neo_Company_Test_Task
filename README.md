# Rest Api Controller для тестового задания от компании Нео

Это проект на .NET 6.0, который состоит буквально из одного контроллера и выполняет следующую задачу:
1. Принимает POST-запрос с параметрами синусоидального сигнала: A - амплитуда в условных единицах, Fd - частота дискретизации сигнала в точках за секунду, Fs - частота сигнала в герцах, N - количество периодов.
2. Формирует графический файл, содержащий синусоидальный сигнал по заданным параметрам и возвращает его клиенту (после отправки запроса в браузере файл должен быть скачан).

## Что я использовал для решения данной задачи

1. В решение встроен Swagger для упрощения проверки задания проверяющему. А так в принципе достаточно поднять локальный сервер заэкзпозить локальный порт и просто отправить запрос через Postman.
   
2. Тело запроса я решил вбивать в формате JSON из-за его ряда преимуществ:
*Читаемость: JSON легко читать и понимать для людей благодаря своему структурированному и упорядоченному формату.
*Структура данных: JSON обеспечивает поддержку вложенных структур данных, таких как массивы и объекты. Это делает передачу более сложных структур или групп данных гораздо проще и удобнее.
*Широко поддерживается: JSON широко поддерживается большинством веб-технологий, языков программирования и API, что облегчает интеграцию с различными программными системами.
*Экономия трафика: JSON обычно требует меньше места по сравнению с другими форматами, такими как XML, что снижает объем передачи данных и ускоряет обмен данными между клиентом и сервером.
*Безопасность типов: JSON способствует изоляции типов данных, таким образом при разборе JSON-сообщения значения могут быть доразобраны в нужный тип данных, что может уменьшить возможность атак.

3. Для работы с графикой я использовал библиотеку SkiaSharp. Почему именно её спросите вы. Ответ очевиден. Во первых пространство имён System.Drawing начиная с .NET 6.0 не поддерживается в линуксовых системах, а с 7.0 даже в Windows. В итоге на официальном сайте Microsoft нам предлагают ряд библиотек для замены функционала System.Drawing. А именно ImageSharp, SkiaSharp, Компоненты образов Windows и Microsoft.Maui.Graphics. Я решил всё таки использовать SkiaSharp из-за его ряда преимуществ:
-Кроссплатформенность: SkiaSharp поддерживает множество платформ, включая Android, iOS, macOS, Windows и Linux.
*Быстрота: SkiaSharp позволяет создавать высокопроизводительные приложения с качественной графикой.
*Открытый исходный код и бесплатность: SkiaSharp распространяется под лицензией Apache 2.0, что означает, что она бесплатна и имеет открытый исходный код.
*Простота использования: SkiaSharp основана на знакомом и интуитивно понятном API, таком как System.Drawing из .NET.
*Мощные возможности: SkiaSharp обладает мощными возможностями для работы с графикой, включая возможность создания всех типов изображений и форматов, рендеринг текста, работа с пикселями, трансформациями растровых изображений и поддержка шейдеров.
*Интеграция с Xamarin: SkiaSharp может использоваться в совместной работе с Xamarin и платформами мобильной разработки.

4. Всё решение я контейнезировал используя такую технологию как Docker. Это было сделано из-за ряда причин:
*Переносимость приложений: Docker контейнеры стандартизированы и могут запускаться на любой платформе, что позволяет разрабатывать приложения в одной среде и запускать их в другой без изменений.
*Упрощение конфигурации и управления: Docker контейнеры содержат всю необходимую для работы приложения информацию, включая зависимости и конфигурационные файлы, что упрощает их управление и поддержку.
*Более быстрое развертывание приложений: Docker позволяет быстро разворачивать новые экземпляры приложений, благодаря тому, что они могут быть созданы на основе предварительно настроенного образа контейнера.
*Изоляция приложения: Docker контейнеры изолируют приложения от других приложений и даже других контейнеров на том же хосте. Это означает, что приложения могут быть запущены более безопасно и не будут влиять на другие приложения.
*Экономия ресурсов: Docker позволяет запускать несколько контейнеров на одном хосте, что позволяет использовать ресурсы более эффективно и сокращает затраты на хостинг.
*Более эффективное управление: Docker предоставляет набор инструментов для управления контейнерами, в том числе инструменты для мониторинга, логирования и масштабирования приложений.

А так же это было сделано для того, чтобы в последствии использовать созданный контейнер в Kubernetes. У которого тоже невероятное количество плюсов:
*Автоматизированное развертывание и управление: Kubernetes позволяет автоматически разворачивать и управлять контейнеризированными приложениями, что упрощает управление приложениями и позволяет сократить затраты времени и усилий на их настройку и управление.
*Масштабирование: Kubernetes позволяет горизонтально масштабировать приложения, что позволяет легко обрабатывать более высокие нагрузки без изменения кода приложения.
*Отказоустойчивость: Система автоматически перезапускает приложения, которые выходят из строя, и автоматически мигрирует приложения с одного узла на другой в случае сбоев узлов.
*Регулирование трафика: Kubernetes позволяет управлять трафиком, регулировать его объем и направление, а также приостанавливать или рестартовать сервисы при необходимости.
*Конфигурирование и управление хранилищами данных: Kubernetes предоставляет удобный и простой интерфейс для управления хранилищами данных.
*Открытость и общность: Kubernetes является Open Source проектом и имеет большую и активную "общину", что позволяет получить поддержку и помощь от участников со всего мира.

P.S. Если у вас контейнеризация Docker происходит в Windows, то тогда советую удалить библиотеку SkiaSharp.NativeAssets.Linux.NoDependencies и удалить эту строчку из файла Docker:
-RUN apt-get update && apt-get install -y libegl1-mesa
P.S.S. Так же советую удалить эту строчку если в вашем контейнере уже есть пакеты для работы с OpenGL. Просто библиотека SkiaSharp в конечном итоге ссылается на любой линуксовый пакет для работы с OpenGL. Хотя на форумах и пишут что достаточно установить библиотеку SkiaSharp.NativeAssets.Linux.NoDependencies, но похоже если в самой системе контейнера нет самой простой библиотеки для работы с OpenGL, то он просто не может построить графическое изображение. Так-же пробовал запускать контейнер на Alpine и менять базовый образ .NET 6.0  с поддержкой именно Alpine, а не Debian, но это тоже большую роль не сыграло.

## Пример входных данных

```json
{
  "a": 50,
  "fd": 44100,
  "fs": 440,
  "n": 10
}
```

## Как работать с приложением

По сути вам достаточно просто запустить отладку в режиме Debug и если у вас установлен Docker, то выбрать именно его вариант. С Docker файлом по идее проблем быть не должно если у вас контейнеризация просиходит в Linux. Но если нет, то я писал что можно сделать выше в пунктах P.S. А если у вас нет Docker, то выберите вариант отладки так же в режиме Debug, но на этот раз с вариантом IIS Express. Дальше у вас открывается интерфейс Swagger и вы просто вводите данные в формате JSON в POST запрос, пример данных был написан выше.

### Заключительная часть. А именно что можно было бы исправить или изменить.
1. Можно поиграться с файлом Docker, так как в данном решении он устанавливает много ненужных для работы микросервиса пакетов и зависимостей. И в итоге микросервис весит немного больше и жрёт ресурсы побольше. Разница конечно будет не критическая, но место оптимизации имеет быть.
2. В код функции GenerateImage, которая находится в ImageGeneratorController и отлавливает POST запрос можно добавить конструкцию try catch finally, но так как это просто прикладное решение чисто для примера + ошибки можно прекрасно видеть в результате запроса в Swagger, то я считаю это не имеет смысла. Так же это довольно безобидный запрос, над которым даже если изощраться, то он никак не нарушит работу микросервиса или его целостность.
