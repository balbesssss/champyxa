CREATE EXTENSION IF NOT EXISTS pgcrypto;

create table Roles(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(50) not null unique
)

create table Departments(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100) not null unique
)

create table Users(
    Id UUID primary key default gen_random_uuid(),
    Name varchar (100) not null,
    PasswordHash varchar(150) not null,
    RoleId references Roles(Id),
    DepartmentId references Departments(Id),
    CreatedAt timestamp default now()
)

create table Products (
    Id uuid primary key default gen_random_uuid(),
    Code varchar(100) not null unique,
    Name varchar(100) not null,
    Type varchar(100) not null,
    Form varchar(100) not null,
    Status varchar(100) not null,
    CreatedAt timestamp not null default now(),

    Constraint CK_PSTATUS Check(Status in ('active','archived') ),
    Constraint CK_TYPE Check(Type in ('гербицид', 'инсектицид', 'фунгицид'))
) 

create table Recipes(
    Id uuid primary key default gen_random_uuid(),
    ProductId uuid references Products(Id),
    Version int not null,
    Status varchar(100),
    CreatedAt timestamp not null default now(),
    Comment text,

    Constraint CK_RSTATUS check (Status in ('черновик', 'утверждено', 'заархивировано'))
)

create table Equipments(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100),
    Type varchar(100),
    Line varchar(100)
)

create table TechCards (
    Id uuid primary key default gen_random_uuid(),
    ProductId uuid references Products(Id),
    Version int,
    Status varchar(100),
    CreatedAt timestamp default now(),

    Constraint CK_TSTATUS check (Status in ('черновик', 'утверждено', 'заархивировано'))
)

create table ProductionOrders(
    Id uuid primary key default gen_random_uuid(),
    ProductId uuid references Products(Id),
    RecipeId uuid references Recipes(Id),
    TechCardId uuid references TechCards(Id),
    Quantity decimal,
    Status varchar(50),
    CreatedAt timestamp default now(),
    CreatedBy uuid references Users(Id),
    Constraint CK_PQUANTITY check (Quantity > 0),
    Constraint CK_PRSTATUS check (Status in ('новый','в_процессе','сделано','отменено'))
)

create table RawMaterials (
    Id uuid primary key default gen_random_uuid(),
    Code varchar(100) unique,
    Name varchar(100),
    Unit varchar(100)
)

create table RawMaterialBatches(
    Id uuid primary key default gen_random_uuid(),
    RawMaterialId references RawMaterials(Id),
    SupplierBatchNumber varchar(100) unique,
    Supplier varchar(100),
    ReceivedAt timestamp default now(),
    Quantity decimal,
    Unit varchar(100),
    LabStatus varchar(100),
    Constraint CK_PQUANTITY check (Quantity > 0),


    Constraint CK_RLAB_STATUS check (LabStatus in ('ожидающий', 'в_процессе', 'одобрено', 'заблокировано'))
)

create table ProductBatches(
    Id uuid primary key default gen_random_uuid(),
    ProductionOrderId uuid references ProductionOrders(Id),
    BatchNumber varchar(100) unique,
    Status varchar(100),
    StartedAt timestamp  default now(),
    FinishedAt timestamp null,
    EquipmentId references Equipments(Id),

    Constraint CK_PRO_STATUS check (Status in ('созданный', 'в_процессе', 'одобрено', 'заблокировано'))
)

create table BatchRawMaterials(
    Id uuid primary key default gen_random_uuid(),
    ProductBatchId references ProductBatches(Id),
    RawMaterialBatcheId references RawMaterialBatches(Id),
    QuantityUsed decimal,

    Constraint CK_BQUANTITY check (QuantityUsed > 0)
)

create table TechSteps(
    Id uuid primary key default gen_random_uuid(),
    TechCardId references TechCards(Id),
    StepOrder int,
    Name varchar(100),
    TypeStep varchar(100),
    IsMandatory boolean,
    Instructions text
)

create table BatchSteps (
    Id uuid primary key default gen_random_uuid(),
    ProductBatchId references ProductBatches(Id),
    TechStepId references TechSteps(Id),
    Status varchar(100),
    StartedAt timestamp default now(),
    FinishedAt timestamp null,
    StartedBy references Users(Id),
    Comment text,

    Constraint CK_BSTATUS check (Status in ('ожидание', 'в_процессе', 'завершено', 'сбой'))
)

create table StepParameters(
    Id uuid primary key default gen_random_uuid(),
    TechStepId references TechSteps(Id),
    Name varchar(100),
    Unit varchar(100),
    MinValue decimal,
    MaxValue decimal,
    IsRequired boolean
)

create table BatchStepValues(
    Id uuid primary key default gen_random_uuid(),
    BatchStepId references BatchSteps(Id),
    StepParamterId references StepParameters(Id),
    Value decimal,
    CreatedAt timestamp default now()
)

create table Deviations(
    Id uuid primary key default gen_random_uuid(),
    BatchStepId references BatchSteps(Id),
    StepParameterId references StepParameters(Id),
    PlannedValue decimal,
    ActualValue decimal,
    Severity varchar(100),
    CreatedAt timestamp default now(),
    Comment varchar(100),

    Constraint CK_DSEVERITY check (Severity in ('утвержден','заблокирован'))
)

create table Events(
    Id uuid primary key default gen_random_uuid(),
    EventType varchar(100),
    EntityType varchar(100),
    EntityId uuid default gen_random_uuid(),
    UserId references Users(Id),
    Message text,
    CreatedAt timestamp default now()   
)

create table LabDecisions(
    Id uuid primary key default gen_random_uuid(),
    RawMaterialBatcheId references RawMaterialBatches(Id),
    ProductBatchId references ProductBatches(Id),
    Decision varchar(100),
    Comment text,
    CreatedAt timestamp default now(),
    CreatedBy references Users(Id)
)

create table LabTests(
    Id uuid primary key default gen_random_uuid(),
    Type varchar(100),
    OdjectType varchar(100),
    RawMaterialBatchId references RawMaterialBatches(Id) on delete restrict,
    ProductBatchId references ProductBatches(Id), 
    Status varchar(100),
    Priority varchar(100),
    CreatedAt timestamp default now(),
    CreatedBy references Users(Id),
    Comment text,

    Constraint CK_LSTATUS check (Status in ('созданный', 'в_процессе', 'одобрено', 'заблокировано')),
    Constraint CK_PRIORITY check (Priority in ('нормальный','высокий','критический'))
)

create table LabTestParameters(
    Id uuid primary key default gen_random_uuid(),
    LabTestId references LabTests(Id) on delete cascade,
    Name varchar(100),
    Unit varchar(100),
    MinValue decimal,
    MaxValue decimal,
    ActualValue decimal,
    IsRequired boolean
)

create table RecipeComponents(
    Id uuid primary key default gen_random_uuid(),
    RecipeId references Recipes(Id) on delete cascade,
    RawMaterialId references RawMaterials(Id),
    Share decimal,
    LoadOrder int,

    Constraint CK_RSHARE check (Share > 0 and Share <=100)
)
