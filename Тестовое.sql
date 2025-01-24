--������� 2

--������� �������
drop table if exists #Clients;
create table #Clients
(
  Id int Identity(1,1), -- Id �������
  ClientName nvarchar(255) -- ������������ �������
)

--������� �������� ��������
drop table if exists #ClientContacts;
create table #ClientContacts
(
  Id bigint Identity(1,1), -- Id ��������
  ClientId bigint, -- Id �������
  ContactType nvarchar(255), -- ��� ��������
  ContactValue nvarchar(255) --  �������� ��������
)

insert into #Clients values ('������'),('������'),('�������'),('������'),('�������')
insert into #ClientContacts values (1,'�����','mail'),(2,'�����','mail1'),(3,'�������','9999999'),(4,'�������','5555555'),(5,'�������','8888888'),(5,'�����','mail2'),(2,'�������','8888888'),(2,'�������','9888888')

--�������� ������, ������� ���������� ������������ �������� � ���-�� ��������� ��������

select c.ClientName, count(cc.Id) AS ContactCount
from #Clients c
inner join #ClientContacts cc on c.Id = cc.ClientId
group by c.ClientName, cc.ClientId

--�������� ������, ������� ���������� ������ ��������, � ������� ���� ����� 2 ���������

select c.ClientName
from #ClientContacts cc
inner join #Clients c on c.Id = cc.ClientId
group by cc.ClientId,c.ClientName
having count(cc.ContactValue) >2

--������� 3
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