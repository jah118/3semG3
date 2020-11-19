CREATE TABLE [User] (
	[id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,[username] nvarchar(64) unique not null
	,[passwordHash] binary(64) NOT NULL
	,[salt] binary(64) not null
)

ALTER TABLE Person 
	ADD UserID int
	,FOREIGN KEY (UserID) references [User](id)
