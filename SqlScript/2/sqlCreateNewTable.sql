DROP TABLE [QUEUES];
DROP TABLE [USER];
CREATE TABLE [USER] (
    Id INT PRIMARY KEY IDENTITY,
    UserName VARCHAR(20) NOT NULL,
    Password VARCHAR(10) NOT NULL,
    CHECK (LEN(UserName) > 2),
    CHECK (LEN(Password) >= 6)
);

CREATE TABLE [QUEUES] (
    Id INT PRIMARY KEY IDENTITY,
   
    UserName VARCHAR(20) NOT NULL,
    AppointmentDate DATE NOT NULL,
    AppointmentTime TIME NOT NULL
);
select * from [dbo].[USER]
DROP TABLE [QUEUES];
CREATE TABLE [QUEUES] (
    Id INT PRIMARY KEY IDENTITY,
   
    UserName VARCHAR(20) NOT NULL,
    AppointmentDate VARCHAR(20) NOT NULL,
    AppointmentTime VARCHAR(20) NOT NULL
);
select *from [QUEUES]
