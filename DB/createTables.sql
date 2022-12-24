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
VALUES  (0,'Понедельник'),
(1,'Вторник'),
(2,'Среда'),
(3,'Четверг'),
(4,'Пятница'),
(5,'Суббота'),
(6,'Воскресенье')
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
('Брейк-данс','Танец драйва, адреналина, пропитанный духом соперничества и лидерства. Для сильных и независимых B-boys и B-girls, для экстремалов, способных показать себя и поразить других!','БРЕЙК ДАНС (Брейкинг, Бибоинг) — уличный танец, одно из течений хип-хоп культуры.
Breakdance (Breaking) - это один из самых современных, динамичных и экстремальных стилей. Нижний брейк захватывает дух и владеет умами огромной части молодежи во всем мире. Ни в одном танце нет стольких акробатических элементов, как в брейк-дансе.', 3700,'static/images/breakdance.jpg'),
('Хип-Хоп','Сегодня хип-хоп – это самый модный и популярный тренд молодежной культуры. Этот стиль впитал в себя уличную философию афроамериканцев, элементы фанка, попа, брейка, джаза.','Танец ХИП-ХОП – пожалуй один из самых популярых среди молодежи. Хип-хоп танцуют и на улице и в клубах, его можно часто увидеть в клипах и фильмах.
Hip-hop очень разнообразный и многогранный танец. Он сочетает в себе пластичность, работу на разных уровнях, резкость и скорость, оставаясь при этом стильным и простым в освоении.',3600,'static/images/hiphop.webp'),
('Джаз-фанк','Уникальная смесь из интересных элементов других танцевальных стилей, которые, на первый взгляд, не совместимы между собой.','Танец Джаз Фанк (Jazz Funk) – это уникальная смесь из интересных элементов других танцевальных стилей, которые, на первый взгляд, не совместимы между собой. Это и делает танец таким свободным и популярным. Jazz Funk не имеет ограничений и правил, что позволяет выразить в нем все что угодно.
В первую очередь, Jazz-funk, это манера, экспрессивность и креативность в танце. Это ваш собственный стиль и только ваше восприятие музыки.',3600,'static/images/jazzfunk.jpg'),
('Контемп','Отсутствие рамок в контемпорари открывает бескрайние горизонты для самовыражения в танце, дарит потрясающие физические возможности и незабываемые впечатления от танца.','Contemporary dance (контемпорари) — дословно переводится, как современный танец. Этому направлению дано еще одно не очень привычное для слуха название – «танец души», ведь танцевальные движения «говорят» со зрителями с помощью пластики.',3700,'static/images/Kontemporari.jpg'),
('Стрип-пластика','Стрип - это танец женственности и раскрепощения. Это страсть, нежность, откровенность..','Стрип (Strip-пластика, Strip-dance) – это танец женственности и раскрепощения. Это страсть, нежность, откровенность. 
Научиться в совершенстве владеть своим телом и просто «излучать» уверенность и обольстительность - вот главная цель танца Стрип. Каждая девушка хочет быть женственной, интересной, красивой и волнующей для противоположного пола. Изящность и грациозность в движениях отличают любую девушку, занимающуюся стрип пластикой, и не позволяют ей пройти незамеченной. На занятиях по стрипу не нужно обнажаться, это миф. Вся роскошь этого стиля в том, чтобы в танце оставалась некая тайна, недосказанность. В сочетании с бесконечной чувственностью, сексуальностью и грациозностью движений, действительно сложно представить более завораживающий танец, чем стрип-дэнс.',3700,'static/images/strip1.jpg'),
('Вог','Точная быстрая техника, четкие линии, сочетающиеся со свободным телом, манерной походкой и артистичной игрой.','Танец Вог (Vogue) – сильно стилизованный современный танец, характеризующийся фотомодельными позами. Vogue в переводе с английского означает «мода». В танце используются характерные позы рук, которые образуют четкие углы и линии. Движения тела также сильные, четкие, а походка грациозная и кокетливая.',3500,'static/images/vogue.jpg')
select * from DanceStyle


delete from Coaches
DBCC CHECKIDENT (Coaches, RESEED, 100)
insert into Coaches(FirstName,Surname,AboutCoach,PicturePath)
values ('Анна','Соколова','Для меня танец - это тот мир, в котором можно создавать свою реальность, становясь по настоящему свободным и счастливым человеком.','static/images/coach1.jpg'),
('Марина','Щербакова','В этом мире на тебя наваливается столько разных проблем, а когда ты танцуешь, ты свободен. Я подарю тебе эту свободу.','static/images/coach2.jpg'),
('Александр','Карасев','Я не притворяюсь. Когда я танцую, я такой, какой я есть.','static/images/coach3.jpg')
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





