using Api.Helpers.Mapping;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingService));
builder.Services.AddTransient<IStudentRepository, StudentRepository>();



// Setup Logging
builder.Logging.AddConsole();

//CORS POLICY
builder.Services.AddCors(options => options.AddPolicy("DevelopmentApi",
    policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts")!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    })
);
// DB CONTEXT // TODO FIX THIS FOR DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(dbOptions =>
{
    dbOptions.UseSqlite(connectionString,
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DevelopmentApi");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Scope for DB Service and Migration   // TODO SCOPE FOR DB
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception error)
{
    // Throw Error
}


app.Run();
