using System.Security.Claims;
using System.Text;
using api.db.Repo;
using api.DB.Models;
using api.DTO;
using api.Responses;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Npgsql.Replication.PgOutput;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<DBRepository>();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
builder.Services.AddScoped<IPasswordHasher<UserDTO>, PasswordHasher<UserDTO>>();


var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapGet("/api/product", async (DBRepository db, int limit = 50, int offset = 0) =>
{
    var items = await db.GetEntities<Products>("Products", limit, offset);
    var total = await db.GetCountEntity("Products");

    return Results.Ok(ApiResponses<IEnumerable<Products>>.Ok(items, pagination: new Pagination (limit, offset,total)));
});
app.MapGet("/api/product/{id}", async (DBRepository db, Guid id) =>
{
    var item = await db.GetEntity<Products>("Products", id);
    return item != null ? Results.Ok(ApiResponses<Products>.Ok(item)) : Results.NotFound(ApiResponses.Fail("Invalid Id",ErrorCode.NotFound));
});
app.MapPost("/api/product", async (DBRepository db, Products p) =>
{
    var result = await db.CreateEntity("Products", p);
    return result > 0 ? Results.Created("/api/product", ApiResponses.Ok("Product add")) : Results.BadRequest(ApiResponses.Fail("invalid body",ErrorCode.));
});
app.MapDelete("/api/product/{id}",async (DBRepository db, Guid id ) =>
{
    var result = await db.DeleteEntity("Products", id);
    return result > 0 ? Results.Ok(ApiResponses.Ok("Product delete")) : Results.BadRequest(ApiResponses.Fail("invalid body"));
    
});
app.MapPatch("/api/product/{id}",async (DBRepository db, Guid id, PatchProduct p) =>
{
    var result = await db.PatchEntity("Products", p, id);
    return result > 0 ? Results.Ok(ApiResponses.Ok("Product edit")) : Results.BadRequest(ApiResponses.Fail("invalid body"));
});

app.MapGet("/api/role", async (DBRepository db, int limit = 50, int offset = 0) =>
{
    var items = await db.GetEntities<Role>("Roles", limit, offset);
    var total = await db.GetCountEntity("Roles");
    return Results.Ok(ApiResponses<IEnumerable<Role>>.Ok(items, new Pagination(limit, offset, total)));
});
app.MapGet("/api/role/{id}", async (DBRepository db, Guid id) =>
{
    var item = await db.GetEntity<Role>("Roles", id); 
    return item is not null? Results.Ok(ApiResponses<Role>.Ok(item)) : Results.BadRequest(ApiResponses.Fail("invalid body"));
     
});
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
app.MapPost("/api/users", async ([FromServices]DBRepository db, [FromServices]IPasswordHasher<UserDTO> hasher, UserDTO user) =>
{
    user.Password = hasher.HashPassword(user, user.Password);
    var result = await db.CreateEntity("Users", user);
    return result > 0 ? Results.Ok(ApiResponses.Ok("User create")) : Results.BadRequest(ApiResponses.Fail("invalid body"));
});

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

app.MapGet("/api/techstep", async (DBRepository db, int limit = 50, int offset = 0 ) => await db.GetEntities<TechStep>("techsteps",limit, offset));
app.MapGet("/api/techstep/{id}", async (DBRepository db, Guid id) => await db.GetEntity<TechStep>("techsteps",id));
app.MapPost("/api/techstep", async (DBRepository db, TechStep r ) => await db.CreateEntity("techsteps",r));
app.MapDelete("/api/techstep/{id}",async (DBRepository db, Guid id ) => await db.DeleteEntity("techsteps",id));
app.MapPatch("/api/techstep/{id}",async (DBRepository db, Guid id, TechStepPatch r ) => await db.PatchEntity("techsteps",r,id));


app.MapPost("/api/auth/login",async ([FromServices]IPasswordHasher<User> hasher,[FromServices]DBRepository db,[FromServices]IConfiguration config,  [FromBody]UserLogin user) =>
{
    var userDB = await db.GetEntityByName<User>("users", user.Name);
    if (userDB is null)
    {
        return Results.Unauthorized();
    }
    var result = hasher.VerifyHashedPassword(userDB, userDB.Password, user.Password);
    if (result == PasswordVerificationResult.Failed) { return Results.Unauthorized(); }
    var token = JWTGen.GenerateToken(userDB, config);
    return Results.Ok(ApiResponses<JWTToken>.Ok(new JWTToken {Token = token, AccessToken = ""}));
});

// app.MapGet("/api/users/me", async (DBRepository db, Htt us) =>
// {

// });

app.Run();
