Create Table "User"(
  UserId Int Identity(1,1) Primary Key,
  Email Nvarchar(100) Not Null, 
  Password Nvarchar(200) Not Null, 
  Role Nvarchar(20) Not Null,
  CreatedAt Date,
  IsDeleted Bit
)

Create Table RefreshToken(
  RefreshTokenId Int Identity(1,1) Primary Key,
  Token Nvarchar(MAX) Not Null, 
  UserId Int
)

Alter Table RefreshToken
Add Foreign Key (UserId) References "User"(UserId)



Create Table Company(
  CompanyId Int Identity(1,1) Primary Key,
  Name Nvarchar(100) Not Null,
  Description Nvarchar(100) Not Null,
  City Nvarchar(100) Not Null,
  IsDeleted Bit
)

Create Table Room(
  RoomId Int Identity(1,1) Primary Key,
  Number Int Not Null,
  Floor Int Not Null,
  Capacity Int Not Null,
  IsDeleted Bit,
  CompanyId Int
)

Alter Table Room
Add Foreign Key (CompanyId) References Company(CompanyId)