# FinBeatTest
Тестовое задание для компании FinBeat

Задача 1
Необходимо реализовать веб-сервис на Asp.Net Core. Информация о запросах и ответов методов должна логгироваться в БД.
Серверная часть
Разработать 2 метода API по технологии REST.
1 метод
Данный метод предназначен для сохранения данных в БД
Краткое описание:
Данный метод получает на вход json вида
[
        	{“1”: “value1”},
        	{“5”: “value2”},
{“10”: “value32”},
….
]
●  	Преобразует его к объекту вида:
o   code – код. Тип int.
o   value – значение. Тип string.
●  	Полученный массив необходимо отсортировать по полю code и сохранить в БД (в решении необходимо описать структуру таблицы).
В таблице должно быть 3 поля:
●  	порядковый номер
●  	код
●  	значение
Перед сохранением данных таблицу необходимо очистить.
2 метод
Возвращает данные клиенту из таблицы в виде json.
Возвращаемые данные:
●  	порядковый номер
●  	код
●  	значение
Добавить возможность фильтрации возвращаемых данных.


Инструкция для задания 1:

1. В файле appsettings.json изменить строку "DefaultConnection" на актуальную строку подключения к MSSQL.
2. Запустить миграции через Package Manager Console(Миграции уже созданы). Команда Update-Database.

Проект выполнен на ASP.Net core с использованием ef core. Логгирование запросов происходит в БД, логгирование ошибок записывается в файл logs/log-{dateTime}.log с помощью Nlog. Для тестирования подключен Swagger.

Инструкция для задания 2-3:

1. Файл для задания 2-3 находится в корне проекта. Название файла Тестовое.sql. Задание выполнено в MSSQL.


