drop database if exists searchholiday;

create database searchholiday;
use searchholiday;

create table housesquestions (
  Id int primary key auto_increment,
  Title varchar(20),
  Question varchar(50),
  RightAnswer text,
  TypeQuestion int,
  IsError boolean
);

create table users (
  Id int primary key auto_increment,
  Email varchar(50),
  Password varchar(50),
  Role text
);

create table housesanswers (
  Id int primary key auto_increment,
  AnswerText varchar(100),
  QuestionsId int,
  foreign key (QuestionsId) references housesquestions (Id) on delete cascade
);

insert into housesquestions (Title, Question, RightAnswer, TypeQuestion, IsError)
values ("Gdgdfs", "fgdgdfsfd", "23", 1, false),
       ("unijmkfd,l", "uoimok,gfldg", "23", 2, false);
       
insert into users (Login, Password, Role)
values ("admin@gmail.com", "123456", "admin"),
       ("user1@mail.com", "743895fdkmog", "user");
     
insert into housesanswers (AnswerText, QuestionsId)
values ("jhjinj", "1"),
       ("gfdgf", "1"),
       ("fgsdgfds", "1"),
       ("jhjinj", "2"),
       ("gfdgf", "2"),
       ("fgsdgfds", "2");
       
select * from housesquestions;
select * from users;
select * from housesanswers;