create table Users(
	Id int not null identity(1001,1),
	UserName varchar(50),
	Email varchar(30) not null,
	EncyptedPassword varchar(255),
	Guid varchar(255),
	constraint pk_user primary key(Id));

create table Coaches(
	Id int not null identity(101,1),
	FirstName varchar(30) not null,
	Surname varchar(50) not null,
	AboutCoach text ,
	PicturePath varchar(255)
	constraint pk_coach primary key(Id));

create table Sessions(
	Id uniqueidentifier ,
	UserId int not null,
	Login varchar(50) not null,
	Password varchar(512) ,
	constraint pk_session primary key(Id));


create table DanceStyle(
	Id int not null identity(1,1),
	StyleName varchar(30) not null,
	ShortDescription text ,
	LongDescription text ,
	LessonCost float not null,
	PicturePath varchar(255) not null default 'static/images/banner.jpg'
	constraint pk_style primary key(Id));


create table StyleCoach(
	CoachId int not null,
	StyleId int not null,
	constraint pk_coach_style primary key(CoachId,StyleId),
	constraint fk_sc_style foreign key(StyleId)   
      references DanceStyle(Id) on delete cascade on update cascade,
	constraint fk_sc_coach foreign key(CoachId)   
      references Coaches(Id) on delete cascade on update cascade)

create table WeekDay(
	DayId int not null,
	DayName varchar(15) not null)

create table LessonTime(
	TimeId int not null,
	TimeNumb int not null,
	LessStart time not null,
	LessEnd time not null
)

create table Schedule(
	DayId int not null,
	LTimeId int not null,
	StyleId int not null,
	CoachId int not null,
	constraint fk_schedule_style foreign key(StyleId)   
      references DanceStyle(Id) on delete cascade on update cascade,
	constraint fk_schedule_coach foreign key(CoachId)   
      references Coaches(Id) on delete cascade on update cascade)

create table Comments(
	Id int not null identity(101,1),
	UserId int not null,
	UserName varchar(100) not null,
	Message text not null,
	constraint pk_comment primary key(Id));


delete from WeekDay
INSERT INTO WeekDay(DayId,DayName)
VALUES  (0,'�����������'),
(1,'�������'),
(2,'�����'),
(3,'�������'),
(4,'�������'),
(5,'�������'),
(6,'�����������')
select * from WeekDay

delete from LessonTime
INSERT INTO LessonTime(TimeId,TimeNumb,LessStart,LessEnd)
VALUES  (0,1,'10:00:00','11:00:00'),
(1,2,'11:00:00','12:00:00'),
(2,3,'12:00:00','13:00:00'),
(3,4,'13:00:00','14:00:00'),
(4,5,'14:00:00','15:00:00'),
(5,6,'15:00:00','16:00:00'),
(6,7,'16:00:00','17:00:00'),
(7,8,'17:00:00','18:00:00'),
(8,9,'18:00:00','19:00:00'),
(9,10,'19:00:00','20:00:00')
select * from LessonTime


delete from DanceStyle
DBCC CHECKIDENT (DanceStyle, RESEED, -1)
INSERT INTO DanceStyle(StyleName,ShortDescription,LongDescription,LessonCost,PicturePath)
VALUES 
('�����-����','����� ������, ����������, ����������� ����� ������������� � ���������. ��� ������� � ����������� B-boys � B-girls, ��� �����������, ��������� �������� ���� � �������� ������!','����� ���� (��������, �������) � ������� �����, ���� �� ������� ���-��� ��������.
Breakdance (Breaking) - ��� ���� �� ����� �����������, ���������� � ������������� ������. ������ ����� ����������� ��� � ������� ����� �������� ����� �������� �� ���� ����. �� � ����� ����� ��� �������� �������������� ���������, ��� � �����-�����.', 3700,'static/images/breakdance.jpg'),
('���-���','������� ���-��� � ��� ����� ������ � ���������� ����� ���������� ��������. ���� ����� ������ � ���� ������� ��������� ���������������, �������� �����, ����, ������, �����.','����� ���-��� � ������� ���� �� ����� ��������� ����� ��������. ���-��� ������� � �� ����� � � ������, ��� ����� ����� ������� � ������ � �������.
Hip-hop ����� ������������� � ������������ �����. �� �������� � ���� ������������, ������ �� ������ �������, �������� � ��������, ��������� ��� ���� �������� � ������� � ��������.',3600,'static/images/hiphop.webp'),
('����-����','���������� ����� �� ���������� ��������� ������ ������������ ������, �������, �� ������ ������, �� ���������� ����� �����.','����� ���� ���� (Jazz Funk) � ��� ���������� ����� �� ���������� ��������� ������ ������������ ������, �������, �� ������ ������, �� ���������� ����� �����. ��� � ������ ����� ����� ��������� � ����������. Jazz Funk �� ����� ����������� � ������, ��� ��������� �������� � ��� ��� ��� ������.
� ������ �������, Jazz-funk, ��� ������, ��������������� � ������������ � �����. ��� ��� ����������� ����� � ������ ���� ���������� ������.',3600,'static/images/jazzfunk.jpg'),
('�������','���������� ����� � ������������ ��������� ���������� ��������� ��� ������������� � �����, ����� ����������� ���������� ����������� � ������������ ����������� �� �����.','Contemporary dance (������������) � �������� �����������, ��� ����������� �����. ����� ����������� ���� ��� ���� �� ����� ��������� ��� ����� �������� � ������ ����, ���� ������������ �������� �������� �� ��������� � ������� ��������.',3700,'static/images/Kontemporari.jpg'),
('�����-��������','����� - ��� ����� ������������� � �������������. ��� �������, ��������, �������������..','����� (Strip-��������, Strip-dance) � ��� ����� ������������� � �������������. ��� �������, ��������, �������������. 
��������� � ������������ ������� ����� ����� � ������ ���������� ����������� � ����������������� - ��� ������� ���� ����� �����. ������ ������� ����� ���� �����������, ����������, �������� � ��������� ��� ���������������� ����. ��������� � ������������ � ��������� �������� ����� �������, ������������ ����� ���������, � �� ��������� �� ������ ������������. �� �������� �� ������ �� ����� ����������, ��� ���. ��� ������� ����� ����� � ���, ����� � ����� ���������� ����� �����, ���������������. � ��������� � ����������� ��������������, �������������� � ������������� ��������, ������������� ������ ����������� ����� �������������� �����, ��� �����-����.',3700,'static/images/strip1.jpg'),
('���','������ ������� �������, ������ �����, ������������ �� ��������� �����, �������� �������� � ����������� �����.','����� ��� (Vogue) � ������ ������������� ����������� �����, ����������������� �������������� ������. Vogue � �������� � ����������� �������� �����. � ����� ������������ ����������� ���� ���, ������� �������� ������ ���� � �����. �������� ���� ����� �������, ������, � ������� ���������� � ����������.',3500,'static/images/vogue.jpg')
select * from DanceStyle


delete from Coaches
DBCC CHECKIDENT (Coaches, RESEED, 100)
insert into Coaches(FirstName,Surname,AboutCoach,PicturePath)
values ('����','��������','��� ���� ����� - ��� ��� ���, � ������� ����� ��������� ���� ����������, ��������� �� ���������� ��������� � ���������� ���������.','static/images/coach1.jpg'),
('������','���������','� ���� ���� �� ���� ������������ ������� ������ �������, � ����� �� ��������, �� ��������. � ������ ���� ��� �������.','static/images/coach2.jpg'),
('���������','�������','� �� �����������. ����� � ������, � �����, ����� � ����.','static/images/coach3.jpg')
select * from Coaches


delete from StyleCoach
insert into StyleCoach(CoachId,StyleId)
values (101,3),
(101,4),
(101,5),
(102,0),
(102,2),
(103,0),
(102,1)
select * from StyleCoach

delete from Schedule
insert into Schedule (DayId,LTimeId,StyleId,CoachId)
values (0,0,1,103),
(0,2,0,102),
(1,0,3,101),
(1,1,5,101),
(2,6,4,101),
(3,3,0,103),
(3,4,1,103),
(4,8,2,102),
(5,6,1,102),
(6,6,1,103),
(6,7,5,101)
select * from Schedule





