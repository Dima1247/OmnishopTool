USE OmnishopFourSoundDB
Go

Delete From tblProducts
Go

DBCC CHECKIDENT ([tblProducts], RESEED, 0);
GO