using MVC_ECommerceTrgovina.Data;
using MVC_ECommerceTrgovina.Interfaces;
using MVC_ECommerceTrgovina.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//naredba koja omoguæava spajanje s bazom
builder.Services.AddDbContext<ApplicationDbContext>();

//definiranje objekta neke klase (app bi radio i bez toga - služi za povezivanje interfejsa)
builder.Services.AddScoped<IItemsRepository, ItemRepository>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
