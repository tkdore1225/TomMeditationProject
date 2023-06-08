Create Database db_TomMeditationsProject;
Drop Database db_TomMeditationsProject;
Use db_TomMeditationsProject;
------------------------------------------------------------
--1 Instructor Table
------------------------------------------------------------
Create Table tb_Instructor
( 
Id int primary key identity,
FullName varchar(50),
InstructorID varchar(50),
PhoneNumber varchar(50),
Email varchar(50),
Location varchar(50)
);

Drop Table tb_Instructor;

Select * From tb_Instructor;

Insert into tb_Instructor( FullName, InstructorID, PhoneNumber, Email, Location)
Values ('Tom Dore', '12345678910111', '+1234232112', 'tdore12@gmail', 'Ca');

Update tb_Instructor
Set FullName='Tommy B'
Where Id = 2;
Select * From tb_Instructor;
-------------------------------------------------------------
--2 Meditation Table
-------------------------------------------------------------
Create Table tb_Meditation
( 
Id int primary key identity,
FullName varchar(50),
MemberID varchar(50),
Duration varchar(50),
Location varchar(50),
Rate varchar(50)
);

Drop table tb_Meditation;
Select * From tb_Meditation;
---------------------------------------------------------------
--3 Sleep Table
---------------------------------------------------------------
Create Table tb_Sleep
( 
Id int primary key identity,
FullName varchar(50),
MemberID varchar(50),
Duration varchar(50),
Date varchar(50)
);

Select * From tb_Sleep;
----------------------------------------------------------------
--4 Mindfulness Table
----------------------------------------------------------------
Create Table tb_Mindfulness
( 
Id int primary key identity,
FullName varchar(50),
MemberID varchar(50),
Description varchar(250),
Date varchar(50)
);

Drop Table tb_Mindfulness;
Select * From tb_Mindfulness;
-----------------------------------------------------------------
--5 Yoga Table
-----------------------------------------------------------------
Create Table tb_Yoga
( 
Id int primary key identity,
FullName varchar(50),
MemberID varchar(50),
Instructor varchar(50),
Duration varchar(50),
Level varchar(50)
);

Drop Table tb_Yoga;
Select * From tb_Yoga;
------------------------------------------------------------------
--6 Prayer Table
------------------------------------------------------------------
Create Table tb_Prayer
( 
Id int primary key identity,
FullName varchar(50),
MemberID varchar(50),
Gratitude varchar(250)
);

Select * From tb_Prayer;
