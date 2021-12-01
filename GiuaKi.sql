use master
go 
create database CenterManager
go
use CenterManager
go 
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

