CREATE EXTENSION IF NOT EXISTS pgcrypto;

create table Roles(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(50) not null unique
);

create table Departments(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100) not null unique
);

create table Users(
    Id UUID primary key default gen_random_uuid(),
    Name varchar (100) not null,
    PasswordHash varchar(150) not null,
    RoleId uuid references Roles(Id) not null,
    DepartmentId uuid references Departments(Id) not null,
    CreatedAt timestamp default now()
);

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
) ;

create table Recipes(
    Id uuid primary key default gen_random_uuid(),
    ProductId uuid references Products(Id) not null,
    Version int not null,
    Status varchar(100) not null,
    CreatedAt timestamp not null default now(),
    Comment text,

    Constraint CK_RSTATUS check (Status in ('черновик', 'утверждено', 'заархивировано'))
);

create table Equipments(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100) not null,
    Type varchar(100) not null,
    Line varchar(100)
);

create table TechCards (
    Id uuid primary key default gen_random_uuid(),
    ProductId uuid references Products(Id) not null,
    Version int not null,
    Status varchar(100) not null,
    CreatedAt timestamp default now(),

    Constraint CK_TSTATUS check (Status in ('черновик', 'утверждено', 'заархивировано'))
);

create table ProductionOrders(
    Id uuid primary key default gen_random_uuid(),
    ProductId uuid references Products(Id) not null,
    RecipeId uuid references Recipes(Id) not null,
    TechCardId uuid references TechCards(Id) not null,
    Quantity decimal not null,
    Status varchar(50) not null,
    CreatedAt timestamp default now(),
    CreatedBy uuid references Users(Id) not null,
    Constraint CK_PQUANTITY check (Quantity > 0),
    Constraint CK_PRSTATUS check (Status in ('новый','в_процессе','сделано','отменено'))
);

create table RawMaterials (
    Id uuid primary key default gen_random_uuid(),
    Code varchar(100) unique not null,
    Name varchar(100) not null,
    Unit varchar(100) not null
);

create table RawMaterialBatches(
    Id uuid primary key default gen_random_uuid(),
    RawMaterialId uuid references RawMaterials(Id) not null,
    SupplierBatchNumber varchar(100) unique not null,
    Supplier varchar(100) not null,
    ReceivedAt timestamp default now(),
    Quantity decimal not null,
    Unit varchar(100) not null,
    LabStatus varchar(100) not null,
    Constraint CK_RQUANTITY check (Quantity > 0),


    Constraint CK_RLAB_STATUS check (LabStatus in ('ожидающий', 'в_процессе', 'одобрено', 'заблокировано'))
);

create table ProductBatches(
    Id uuid primary key default gen_random_uuid(),
    ProductionOrderId uuid references ProductionOrders(Id) not null,
    BatchNumber varchar(100) unique not null,
    Status varchar(100) not null,
    StartedAt timestamp  default now(),
    FinishedAt timestamp null,
    EquipmentId uuid references Equipments(Id) not null,

    Constraint CK_PRO_STATUS check (Status in ('созданный', 'в_процессе', 'одобрено', 'заблокировано'))
);

create table BatchRawMaterials(
    Id uuid primary key default gen_random_uuid(),
    ProductBatchId uuid references ProductBatches(Id) not null,
    RawMaterialBatchId uuid references RawMaterialBatches(Id) not null,
    QuantityUsed decimal not null,

    Constraint CK_BQUANTITY check (QuantityUsed > 0)
);

create table TechSteps(
    Id uuid primary key default gen_random_uuid(),
    TechCardId uuid not null references TechCards(Id) on delete cascade,
    StepOrder int not null,
    Name varchar(100) not null,
    TypeStep varchar(100) not null,
    IsMandatory boolean,
    Instructions text
);

create table BatchSteps (
    Id uuid primary key default gen_random_uuid(),
    ProductBatchId uuid not null references ProductBatches(Id) on delete cascade,
    TechStepId uuid references TechSteps(Id) not null,
    Status varchar(100) not null,
    StartedAt timestamp default now(),
    FinishedAt timestamp null,
    StartedBy uuid references Users(Id) not null,
    Comment text,

    Constraint CK_BSTATUS check (Status in ('ожидание', 'в_процессе', 'завершено', 'сбой'))
);

create table StepParameters(
    Id uuid primary key default gen_random_uuid(),
    TechStepId uuid not null references TechSteps(Id) on delete cascade ,
    Name varchar(100) not null,
    Unit varchar(100) not null,
    MinValue decimal not null,
    MaxValue decimal not null,
    IsRequired boolean not null
);

create table BatchStepValues(
    Id uuid primary key default gen_random_uuid(),
    BatchStepId uuid not null references BatchSteps(Id) on delete cascade,
    StepParameterId uuid references StepParameters(Id) not null,
    Value decimal not null,
    CreatedAt timestamp default now()
);

create table Deviations(
    Id uuid primary key default gen_random_uuid(),
    BatchStepId uuid not null references BatchSteps(Id) on delete cascade,
    StepParameterId uuid references StepParameters(Id) not null,
    PlannedValue decimal not null,
    ActualValue decimal not null,
    Severity varchar(100) not null,
    CreatedAt timestamp default now(),
    Comment varchar(100) not null,

    Constraint CK_DSEVERITY check (Severity in ('предупреждение','критический'))
);

create table Events(
    Id uuid primary key default gen_random_uuid(),
    EventType varchar(100) not null,
    EntityType varchar(100) not null,
    EntityId uuid default gen_random_uuid(),
    UserId uuid references Users(Id) not null,
    Message text,
    CreatedAt timestamp default now()   
);

create table LabDecisions(
    Id uuid primary key default gen_random_uuid(),
    RawMaterialBatchId uuid references RawMaterialBatches(Id),
    ProductBatchId uuid references ProductBatches(Id),
    Decision varchar(100) not null,
    Comment text,
    CreatedAt timestamp default now(),
    CreatedBy uuid references Users(Id) not null,

    Constraint CK_LabDecision_Object check ( 
        (RawMaterialBatchId is null and ProductBatchId is not null)
        or 
        (RawMaterialBatchId is not null and ProductBatchId is null)
    )
);

create table LabTests(
    Id uuid primary key default gen_random_uuid(),
    Type varchar(100) not null,
    ObjectType varchar(100) not null,
    RawMaterialBatchId uuid references RawMaterialBatches(Id) on delete restrict,
    ProductBatchId uuid references ProductBatches(Id), 
    Status varchar(100) not null,
    Priority varchar(100) not null,
    CreatedAt timestamp default now(),
    CreatedBy uuid references Users(Id) not null,
    Comment text,

    Constraint CK_LSTATUS check (Status in ('созданный', 'в_процессе', 'одобрено', 'заблокировано')),
    Constraint CK_PRIORITY check (Priority in ('нормальный','высокий','критический')),
    CONSTRAINT CK_LabTest_Object 
    CHECK (
        (RawMaterialBatchId IS NOT NULL AND ProductBatchId IS NULL) OR
        (RawMaterialBatchId IS NULL AND ProductBatchId IS NOT NULL)
    )
);

create table LabTestParameters(
    Id uuid primary key default gen_random_uuid(),
    LabTestId uuid not null references LabTests(Id) on delete cascade,
    Name varchar(100) not null,
    Unit varchar(100) not null,
    MinValue decimal not null,
    MaxValue decimal not null,
    ActualValue decimal not null,
    IsRequired boolean not null
);

create table RecipeComponents(
    Id uuid primary key default gen_random_uuid(),
    RecipeId uuid not null references Recipes(Id)  on delete cascade,
    RawMaterialId uuid references RawMaterials(Id) ,
    Share decimal not null,
    LoadOrder int not null,

    Constraint CK_RSHARE check (Share > 0 and Share <=100)
);