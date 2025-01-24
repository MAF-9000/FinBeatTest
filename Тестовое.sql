--Задание 2

--Таблица клиенты
drop table if exists #Clients;
create table #Clients
(
  Id int Identity(1,1), -- Id клиента
  ClientName nvarchar(255) -- Наименование клиента
)

--Таблица контакты клиентов
drop table if exists #ClientContacts;
create table #ClientContacts
(
  Id bigint Identity(1,1), -- Id контакта
  ClientId bigint, -- Id клиента
  ContactType nvarchar(255), -- тип контакта
  ContactValue nvarchar(255) --  значение контакта
)

insert into #Clients values ('Иванов'),('Козлов'),('Сидоров'),('Петров'),('Соколов')
insert into #ClientContacts values (1,'почта','mail'),(2,'почта','mail1'),(3,'телефон','9999999'),(4,'телефон','5555555'),(5,'телефон','8888888'),(5,'почта','mail2'),(2,'телефон','8888888'),(2,'телефон','9888888')

--Написать запрос, который возвращает наименование клиентов и кол-во контактов клиентов

select c.ClientName, count(cc.Id) AS ContactCount
from #Clients c
inner join #ClientContacts cc on c.Id = cc.ClientId
group by c.ClientName, cc.ClientId

--Написать запрос, который возвращает список клиентов, у которых есть более 2 контактов

select c.ClientName
from #ClientContacts cc
inner join #Clients c on c.Id = cc.ClientId
group by cc.ClientId,c.ClientName
having count(cc.ContactValue) >2

--Задание 3
drop table if exists #Dates;
create table #Dates( 
  Id bigint,
  Dt date
);

insert into #Dates
values(1,'20210131'),(1,'20210110'),(1,'20210120'),(2,'20210130'),(2,'20210115');

select*
from #Dates;

with nextDate as (
  select Id, Dt ,lead(Dt) over (partition by Id order by Id,Dt) nextDate
  from #Dates 
    )

select *
from nextDate nd
where nd.nextDate is not null