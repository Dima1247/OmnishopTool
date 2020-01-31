USE OmnishopFourSoundDB
Go

Delete From tblCategories
Go

DBCC CHECKIDENT ([tblCategories], RESEED, 0);
GO