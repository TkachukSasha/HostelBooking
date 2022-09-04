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