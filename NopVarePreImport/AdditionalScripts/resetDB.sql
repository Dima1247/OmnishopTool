USE NopVarePreImport
Go

Delete From tblProductSupplier
Go

DBCC CHECKIDENT (tblProductSupplier, RESEED, 0);
GO

Delete From tblProductCategory
Go

DBCC CHECKIDENT (tblProductCategory, RESEED, 0);
GO

Delete From tblProduct
Go

DBCC CHECKIDENT (tblProduct, RESEED, 0);
GO

Delete From tblSupplier
Go

DBCC CHECKIDENT (tblSupplier, RESEED, 0);
GO

Delete From tblCategory
Go

DBCC CHECKIDENT (tblCategory, RESEED, 0);
GO

Delete From tblUProduct
Go

DBCC CHECKIDENT (tblUProduct, RESEED, 0);
GO