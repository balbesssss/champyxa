using api.db.Repo;
using api.DB.Models;
using api.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<DBRepository>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/api/product", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<Products>("Products",limit,offset));
app.MapGet("/api/product/{id}", async (DBRepository db, Guid id ) => await db.GetEntity<Products>("Products",id));
app.MapPost("/api/product", async (DBRepository db, Products p) => await db.CreateEntity("Products",p));
app.MapDelete("/api/product/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("Products",id));
app.MapPatch("/api/product/{id}",async (DBRepository db, Guid id, PatchProduct p ) => await db.PatchEntity("Products",p,id));

app.MapGet("/api/role", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<Role>("Roles",limit,offset));
app.MapGet("/api/role/{id}", async (DBRepository db, Guid id) => await db.GetEntity<Role>("Roles",id));
app.MapPost("/api/role", async (DBRepository db,CreateDR r) => await db.CreateEntity("roles",r));
app.MapDelete("/api/role/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("Roles",id));
app.MapPatch("/api/role/{id}",async (DBRepository db, Guid id, PatchDR r ) => await db.PatchEntity("Roles",r,id));

app.MapGet("/api/department", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<Department>("departments",limit,offset));
app.MapGet("/api/department/{id}", async (DBRepository db, Guid id) => await db.GetEntity<Department>("Departments",id));
app.MapPost("/api/department", async (DBRepository db, CreateDR d) => await db.CreateEntity("Departments",d));
app.MapDelete("/api/department/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("Departments",id));
app.MapPatch("/api/department/{id}",async (DBRepository db, Guid id, PatchDR d ) => await db.PatchEntity("Departments",d,id));

app.MapGet("/api/users", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<User>("Users",limit, offset));
app.MapGet("/api/users/{id}", async (DBRepository db, Guid id) => await db.GetEntity<User>("Users",id));
app.MapPost("/api/users", async (DBRepository db, User user ) => await db.CreateEntity("Users",user));
app.MapDelete("/api/users/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("Users",id));
app.MapPatch("/api/users/{id}",async (DBRepository db, Guid id, UserPatch u ) => await db.PatchEntity("Users",u,id));

app.MapGet("/api/recipes", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<Recipes>("Recipes",limit, offset));
app.MapGet("/api/recipes/{id}", async (DBRepository db, Guid id) => await db.GetEntity<Recipes>("Recipes",id));
app.MapPost("/api/recipes", async (DBRepository db, Recipes r ) => await db.CreateEntity("Recipes",r));
app.MapDelete("/api/recipes/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("Recipes",id));
app.MapPatch("/api/recipes/{id}",async (DBRepository db, Guid id, RecipesPatch r ) => await db.PatchEntity("Recipes",r,id));

app.MapGet("/api/equipment", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<Equipment>("equipments",limit, offset));
app.MapGet("/api/equipment/{id}", async (DBRepository db, Guid id) => await db.GetEntity<Equipment>("equipments",id));
app.MapPost("/api/equipment", async (DBRepository db, Equipment e ) => await db.CreateEntity("equipments",e));
app.MapDelete("/api/equipment/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("equipments",id));
app.MapPatch("/api/equipment/{id}",async (DBRepository db, Guid id, PatchEquipment e ) => await db.PatchEntity("equipments",e,id));

app.MapGet("/api/techcard", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<TechCard>("techcards",limit, offset));
app.MapGet("/api/techcard/{id}", async (DBRepository db, Guid id) => await db.GetEntity<TechCard>("techcards",id));
app.MapPost("/api/techcard", async (DBRepository db, TechCard r ) => await db.CreateEntity("techcards",r));
app.MapDelete("/api/techcard/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("techcards",id));
app.MapPatch("/api/techcard/{id}",async (DBRepository db, Guid id, TechCardPatch r ) => await db.PatchEntity("techcards",r,id));




app.Run();

