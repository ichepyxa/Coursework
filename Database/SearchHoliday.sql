drop database if exists searchholiday;

create database searchholiday;
use searchholiday;

create table housesquestions (
  Id int primary key auto_increment,
  Title varchar(20),
  Question varchar(50),
  Answers json,
  RightAnswer text,
  IsError boolean
);

create table users (
  Id int primary key auto_increment,
  Login varchar(50),
  Password varchar(50),
  Role text
);

insert into housesquestions (Title, Question, Answers, RightAnswer, IsError)
values ("Gdgdfs", "fgdgdfsfd", '{"questions": ["jhjinj", "gfdgf", "fgsdgfds"]}', "23", false),
       ("unijmkfd,l", "uoimok,gfldg", '{"questions": ["jhjinj", "gfdgf", "fgsdgfds"]}', "23", false);
       
insert into users (Login, Password, Role)
values ("admin", "123456", "admin"),
       ("user1", "743895fdkmog", "user");
select * from housesquestions;
select * from users;