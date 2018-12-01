Название проекта: Cinema

Описание: Приложение для кинотеатра.

Задание 1

Требования к программному коду
•	Приложение должно быть реализовано по принципу «многослойной» архитектуры. Необходимо наличие четко выраженных слоев:

  o	Слой данных (Data Layer).
  
  o	Слой бизнес логики (Domain/Business Layer).
  
  o	Слой представления (Presentation Layer).
  
•	Слой представления должен содержать WPF-приложение, с применением архитектурного шаблона MVVM.

•	Слой данных должен содержать сервисы для взаимодействия с XML-хранилищем данных.

•	Необходимо применить контейнер инверсии зависимостей (произвольный).

Требования к слою бизнес логики

•	Модели предметной области (слой бизнес логики) должны содержать следующую информацию:

o	Фильм.

	Название.

	Дата выпуска.

•	Год.

•	Месяц.

•	День.

	Язык.

	Актеры.

•	Имя.

•	Фамилия.

	Режиссер.

•	Имя.

•	Фамилия.

Требования к слою представления

•	WPF-приложение должно предоставлять возможность просматривать все существующие фильмы и добавлять новые.

•	Интерфейс приложения может быть произвольным, но при этом отказоустойчивым.

Требования к XML-хранилищу данных

•	Структура и количество XML-файлов может быть произвольным.

Требования к Git-репозиторию

•	Необходимо использовать разработку на базе двух веток: master и develop.

•	Ветка master должна хранить последнюю версию приложения (каждое задание будет представлять из себя новою версию: 1.0, 2.0 и т.д.).

•	Каждая фиксация (commit) в master-ветке должна быть помечена специальной меткой (tag) вида “v1.0”, “v2.0” и т.д.

•	Ветка develop должна использоваться для разработке всей функциональности между релизами (новыми версиями) приложения.

•	Допускается создание дополнительных веток при необходимости.

•	Комментарии при фиксациях должны быть ОСМЫСЛЕННЫМИ и описывать произведенные изменения.

Задание 2

Необходимо добавить в существующее приложении новый источник данных (SQL Server базу данных). Доступ к базе данных необходимо осуществлять применяя технологию ADO.NET с использованием интерфейса доступа к базе данных ODBC.

Задание 3

Необходимо заменить интерфейса доступа к базе данных ODBC на SQL Client.

Задание 4

Необходимо добавить возможность указывать вышел ли фильм на Blu-ray.