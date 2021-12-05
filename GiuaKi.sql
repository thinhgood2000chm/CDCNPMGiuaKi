use master
go 
create database CenterManager
go
use CenterManager
go 
/*Details class: join class - class_student - student*/
select class_student.id, class.class_id, class.name, student.student_id, student.name, student.birthYear
from class 
left Join class_student on class.class_id = class_student.class_id
left join student on class_student.student_id = student.student_id
Order by class.name

/*join class - subject - teacher*/
SELECT class.class_id, class.name, class.subject_id, subject.name, class.teacher_id, teacher.name
FROM class
JOIN subject ON class.subject_id = subject.subject_id
JOIN teacher ON class.teacher_id = teacher.teacher_id
Order by class.name

create table student(
	id int identity Primary key,
	student_id nvarchar(50) unique not null,
	name nvarchar(50),
	birthYear int 

)
create table teacher(
	id int identity Primary key,
	teacher_id nvarchar(50) unique not null,
	name nvarchar(50),

)

create table class(
	id int identity Primary key,
	class_id nvarchar(50) unique not null,
	name nvarchar(50),
	subject_id nvarchar(50),
	teacher_id  nvarchar(50),
)

create table subject(
	id int identity Primary key,
	subject_id nvarchar(50) unique not null,
	name nvarchar(50),

)

create table class_student(
	id int identity Primary key,
	student_id nvarchar(50),
	class_id nvarchar(50),
)
create table userLogin(
	username nvarchar(50)  not null  Primary key,
	password nvarchar(50) not null,
)
insert into  userLogin values('admin', 'admin')

select * from userLogin
insert into  student values('1324asd', 'Nam', 2000)
insert into student values('123asd', 'An', 2000)
insert into student values('sdfsz12', 'Hong', 2000)


insert into  teacher values('453', 'Hồng')
insert into teacher values('123asd', 'Phúc')
insert into teacher values('sdfsz12', 'Vang')



insert into  subject values('T1', 'Toán')
insert into subject values('L1', 'Lý')
insert into subject values('H1', 'Hóa')



insert into  class values('89723h', '1b','T1', '453')
insert into class values('2342', '2b','L1', '123asd')
insert into class values('234231', '3x','H1', 'sdfsz12')

insert into class_student values('1324asd', '89723h')
insert into class_student values('1324asd', '2342')
insert into class_student values('sdfsz12', '89723h')
insert into class_student values('123asd', '234231')


use CenterManager
select * from student
select * from teacher