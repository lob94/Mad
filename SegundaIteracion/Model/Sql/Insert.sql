/* 
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

 
USE [miniportal_test];

SET IDENTITY_INSERT Category ON;
insert into Category(categoryId, name) values(1, 'futbol');
insert into Category(categoryId, name) values(2, 'baloncesto');
insert into Category(categoryId, name) values(3, 'tenis');
insert into Category(categoryId, name) values(4, 'natacion');
insert into Category(categoryId, name) values(5, 'hipica');
insert into Category(categoryId, name) values(6, 'atletismo');
insert into Category(categoryId, name) values(7, 'carrera popular');
insert into Category(categoryId, name) values(8, 'motociclismo');
insert into Category(categoryId, name) values(9, 'automovilismo');
insert into Category(categoryId, name) values(10, 'ciclismo');
SET IDENTITY_INSERT Category OFF;

SET IDENTITY_INSERT Event ON;
insert into Event(eventId, categoryId, name, review, eventDate) values (1, 1,'Copa del Rey','Real Madrid - Barcelona', '20191228');
insert into Event(eventId, categoryId, name, review, eventDate) values (2, 2,'Copa del Rey','Real Madrid - Barcelona', '20191126');
insert into Event(eventId, categoryId, name, review, eventDate) values (3, 1,'Champions','Real Madrid - Bayern Munich', '20211228');
insert into Event(eventId, categoryId, name, review, eventDate) values (4, 2,'Liga ACB','Obradoiro - CSKA', '20191128');
insert into Event(eventId, categoryId, name, review, eventDate) values (5, 3,'Copa Davis','España - Francia', '20180921');
insert into Event(eventId, categoryId, name, review, eventDate) values (6, 3,'Wimbledon','Rafael Nadal - Roger Federer', '20180822');
insert into Event(eventId, categoryId, name, review, eventDate) values (7, 3,'Roland Garros','Garbiñe Muguruza - Serena Williams', '20180901');
insert into Event(eventId, categoryId, name, review, eventDate) values (8, 1,'La Liga','Atletico de Madrid - RC Deportivo', '20181026');
insert into Event(eventId, categoryId, name, review, eventDate) values (9, 2,'Liga ACB','Baskonia - Valencia', '20191202');
insert into Event(eventId, categoryId, name, review, eventDate) values (10, 4,'Campeonato de España Absoluto','Contaremos con la presencia de Mireia  Belmonte', '20180801');
insert into Event(eventId, categoryId, name, review, eventDate) values (11, 4,'Campeonato del Mundo Absoluto','Disputado en China, Hangzhou', '20180820');
insert into Event(eventId, categoryId, name, review, eventDate) values (12, 4,'Juego Olimpicos de la Juventud','Disputados en Buenos Aires, Argentina', '20181010');
insert into Event(eventId, categoryId, name, review, eventDate) values (13, 5,'Campeonato de España Absoluto','Club de Campo Villa de Madrid', '20181012');
insert into Event(eventId, categoryId, name, review, eventDate) values (14, 5,'Global Champions Tour','Club de Campo Villa de Madrid', '20180901');
insert into Event(eventId, categoryId, name, review, eventDate) values (15, 5,'Concurso de Saltos Internacional Casas Novas','A Coruña, Casas Novas', '20181210');
insert into Event(eventId, categoryId, name, review, eventDate) values (16, 6,'Campeonato de España Absoluto','Contaremos con la presencia de Ruth Beitia y Bruno Hortelano', '20180901');
insert into Event(eventId, categoryId, name, review, eventDate) values (17, 6,'Campeonato de Europa Absoluto','Madrid', '20180725');
insert into Event(eventId, categoryId, name, review, eventDate) values (18, 6,'Campeonato de España sub-20','Murcia', '20180902');
insert into Event(eventId, categoryId, name, review, eventDate) values (19, 7,'Carrera de la Mujer','A Coruña. La recaudacion de fondos ira destinada a la lucha contra el cancer.', '20180925');
insert into Event(eventId, categoryId, name, review, eventDate) values (20, 7,'Carrera Popular 10k','A Coruña. Maravilloso recorrido a traves del paseo maritimo de esta preciosa ciudad.', '20181021');
insert into Event(eventId, categoryId, name, review, eventDate) values (21, 7,'Cross San Pedro de Visma','A Coruña. Cross a traves del parque de San Pedro de Visma.', '20180730');
insert into Event(eventId, categoryId, name, review, eventDate) values (22, 8,'Gran Premio Valencia MotoGP','Gran Premio Valencia MotoGP, Valencia. MotoGP', '20181101');
insert into Event(eventId, categoryId, name, review, eventDate) values (23, 8,'Gran Premio Jerez Moto2','Gran Premio Red Bull de España, Jerez. Moto2', '20180920');
insert into Event(eventId, categoryId, name, review, eventDate) values (24, 8,'Gran Premio Jerez Moto3','Gran Premio RedBull de España, Jerez. Moto3', '20180920');
insert into Event(eventId, categoryId, name, review, eventDate) values (25, 9,'Gran Premio Formula 1 Barcelona','Gran Premio de Formula 1 en Barcelona, España', '20180901');
insert into Event(eventId, categoryId, name, review, eventDate) values (26, 9,'Gran Premio Rally Dakar','Disputado a traves de varias etapas. Dakar', '20180923');
insert into Event(eventId, categoryId, name, review, eventDate) values (27, 9,'Circuito de AutoCross Galicia','Circuito a traves de toda Galicia de carreras Autocross', '20181010');
insert into Event(eventId, categoryId, name, review, eventDate) values (28, 10,'Tour de Francia 2018','Uno de los eventos mas importantes y esperados del año. Tour de Francia 2018 con la presencia de Nairo Quintana', '20180801');
insert into Event(eventId, categoryId, name, review, eventDate) values (29, 10,'Vuelta a España 2018','Clasica vuelta a España de ciclismo. No te la puedes perder. Despedida de Alberto Contador de la competicion.', '20180923');
insert into Event(eventId, categoryId, name, review, eventDate) values (30, 10,'Giro de Italia 2018','Giro de Italia de ciclismo. Uno de las competiciones mas importantes del año en esta disciplina.', '20181010');
SET IDENTITY_INSERT Event OFF;