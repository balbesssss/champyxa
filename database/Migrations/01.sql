create table Roles (
    Id serial primary key,
    Name varchar(50) not null unique,
    Description Text   
)

create table Department(
    Id serial primary key,
    Name varchar(100) 
)

create table User(
    Id UUID primary key DEFAULT gen_random_uuid(),
    Name varchar(100) not null,
    PasswordHash Text not null,
    RoleId Int not null references Roles(Id),
    DepartmentId int not null references Department(Id),
    CreatedAt Timestamp not null DEFAULT now(),

    constraint check (LENGTH(Name) >= 3) 
)

create table Products(
    Id UUID primary key DEFAULT gen_random_uuid(),
    Code varchar(25) not null,
    ProductName varchar(100) not null,
    ProductType varchar(100) not null,
    ProductForm varchar(100) not null,
    ProductStatus varchar(50) not null,
    
)