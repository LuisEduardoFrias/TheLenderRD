

Create DataBase TheLenderRD
Go

use TheLenderRD
Go

Create Table AgeRates
(
	Id int identity,
	Age int primary key not null,
	Rate decimal not null,
)
GO

Create Table Months
(
	Id int unique identity,
	Description varchar(15) not null,
	Value int  Primary key not null
)
GO

Create Table Logs
(
	QuryId int primary key identity,
	ConsultationDate Datetime not null,
	Edad int not null,
	Amount decimal not null,
	AccountValue decimal not null,
	QueryIp char(15) not null,
	MonthId int,
	foreign key (MonthId) references Months(Value) 
)
Go

Create proc GetAgeRates
as
Begin

	Select * From AgeRates

End
GO

Create proc GetMonths
as
Begin

	Select * From Months

End
GO

Create proc InsertLog
@ConsultationDate Datetime,
@Edad int,
@Amount decimal,
@AccountValue decimal,
@QueryIp char(15),
@MonthId int
as
Begin

	Insert Into Logs values 
	(
		@ConsultationDate,
		@Edad,
		@Amount,
		@AccountValue,
		@QueryIp,
		@MonthId
	)

End
GO