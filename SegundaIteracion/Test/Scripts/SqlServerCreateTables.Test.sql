/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'miniportal_test')
DROP DATABASE [miniportal_test]


USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [miniportal_test] 
    ON PRIMARY ( NAME = miniportal_test, FILENAME = "' + @Default_DB_Path + N'miniportal_test.mdf")
    LOG ON ( NAME = miniportal_test_log, FILENAME = "' + @Default_DB_Path + N'miniportal_test_log.ldf")'

EXEC(@sql)
PRINT N'Database [MiniPortal_test] created.'
GO


USE [miniportal_test]


/* ********** Drop Table UserProfile if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UsersAndGroups]') AND type in ('U'))
DROP TABLE [UsersAndGroups]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CommentsAndLabels]') AND type in ('U'))
DROP TABLE [CommentsAndLabels]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recommendation]') AND type in ('U'))
DROP TABLE [Recommendation]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Event]') AND type in ('U'))
DROP TABLE [Event]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Label]') AND type in ('U'))
DROP TABLE [Label]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserGroup]') AND type in ('U'))
DROP TABLE [UserGroup]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */




/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */
CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)


CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

/*Group*/
CREATE TABLE UserGroup (
	groupId bigint IDENTITY(1,1) NOT NULL,
	name varchar(30) NOT NULL,
	description varchar(255) NOT NULL,

	CONSTRAINT [PK_UserGroup] PRIMARY KEY (groupId),
	CONSTRAINT [UniqueKey_UserGroup_Name] UNIQUE (name)
)

CREATE NONCLUSTERED INDEX [IX_GroupIndexByName]
ON [UserGroup] ([name] ASC)

/*UsersAndGroups*/
CREATE TABLE UsersAndGroups (
	userId bigint NOT NULL,
	groupId bigint NOT NULL,

	CONSTRAINT [PK_UsersAndGroups] PRIMARY KEY (userId, groupId),
	CONSTRAINT [FK_UsersAndGroups_User] FOREIGN KEY (userId) REFERENCES UserProfile ON DELETE CASCADE,
	CONSTRAINT [FK_UsersAndGroups_UserGroup] FOREIGN KEY (groupId) REFERENCES UserGroup ON DELETE CASCADE

)

CREATE NONCLUSTERED INDEX [IX_UsersAndGroupsIndexByGroupId]
ON [UsersAndGroups] ([groupId] ASC)


/*Category*/
CREATE TABLE Category (
	categoryId bigint IDENTITY(1,1) NOT NULL,
	name varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId),
	CONSTRAINT [Unique_Category_Name] UNIQUE (name)
)

CREATE NONCLUSTERED INDEX [IX_CategoryIndexByName]
ON [Category] ([name] ASC)


/*Event*/
CREATE TABLE Event (
	eventId bigint IDENTITY(1,1) NOT NULL,
	categoryId bigint,
	name varchar(30) NOT NULL,
	review varchar(255),
	eventDate date NOT NULL,

	CONSTRAINT [PK_Event] PRIMARY KEY (eventId),
	CONSTRAINT [FK_Event_Category] FOREIGN KEY (categoryId) REFERENCES Category ON DELETE SET NULL
)

CREATE NONCLUSTERED INDEX [IX_EventIndexByName]
ON [Event] ([name] ASC)


/*Recommendation*/
CREATE TABLE Recommendation (
	recommendationId bigint IDENTITY(1,1) NOT NULL,
	userId bigint,
	groupId bigint,
	eventId bigint,
	reason varchar(50),
	created date NOT NULL,

	CONSTRAINT [PK_Recommendation] PRIMARY KEY (recommendationId),
	CONSTRAINT [FK_Recommendation_User] FOREIGN KEY (userId) REFERENCES UserProfile ON DELETE SET NULL,
	CONSTRAINT [FK_Recommendation_UserGroup] FOREIGN KEY (groupId) REFERENCES UserGroup ON DELETE SET NULL,
	CONSTRAINT [FK_Recommendation_Event] FOREIGN KEY (eventId) REFERENCES Event ON DELETE CASCADE
)


/*Comment*/
CREATE TABLE Comment (
	commentId bigint IDENTITY(1,1) NOT NULL,
	userId bigint,
	eventId bigint,
	loginName varchar(30) NOT NULL,
	content varchar(255),
	commentDate date NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	CONSTRAINT [FK_Comment_User] FOREIGN KEY (userId) REFERENCES UserProfile ON DELETE SET NULL,
	CONSTRAINT [FK_Comment_Event] FOREIGN KEY (eventId) REFERENCES Event ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_CommentIndexByEventId]
ON [Comment] ([eventId] ASC)


/*Label*/
CREATE TABLE Label (
	labelId bigint IDENTITY(1,1) NOT NULL,
	name varchar(30) NOT NULL,

	CONSTRAINT [PK_Label] PRIMARY KEY (labelId),
	CONSTRAINT [Unique_Label_Name] UNIQUE (name)
)

CREATE NONCLUSTERED INDEX [IX_LabelIndexByName]
ON [Label] ([name] ASC)

/*CommentsAndLabels*/
CREATE TABLE CommentsAndLabels (
	labelId bigint NOT NULL,
	commentId bigint NOT NULL,

	CONSTRAINT [PK_CommentsAndLabels] PRIMARY KEY (labelId, commentId),
	CONSTRAINT [FK_CommentsAndLabels_Label] FOREIGN KEY (labelId) REFERENCES Label ON DELETE CASCADE,
	CONSTRAINT [FK_CommentsAndLabels_Comment] FOREIGN KEY (commentId) REFERENCES Comment ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_CommentsAndLabelsIndexByCommentId]
ON [CommentsAndLabels] ([commentId] ASC)


PRINT N'Table UserProfile created.'
PRINT N'Table Event created.'
PRINT N'Table UserGroup created.'
PRINT N'Table Category created.'
PRINT N'Table Comment created.'
PRINT N'Table Label created.'
PRINT N'Table LabelsAndComments created.'
PRINT N'Table UsersAndGroups created.'
PRINT N'Table Recommendation created.'
GO


