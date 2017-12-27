﻿/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *      Configure within the CREATE DATABASE sql-sentence the path where 
 *      database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 
USE [miniportal];

delete from Category;
delete from Event;

insert into Category(name) values('futbol');
insert into Category(name) values('baloncesto');


insert into Event(categoryId, name, review, eventDate) values (1,'Copa del Rey','Real Madrid - Barcelona', '20191228');
insert into Event(categoryId, name, review, eventDate) values (2,'Copa del Rey','Real Madrid - Barcelona', '20191126');
insert into Event(categoryId, name, review, eventDate) values (1,'Champions','Real Madrid - Bayern Munich', '20211228');