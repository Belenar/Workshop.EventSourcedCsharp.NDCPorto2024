using BeerSender.Web.EventPersistence;
using BeerSender.Web.Extensions;
using BeerSender.Web.Hubs;
using BeerSender.Web.Projections;
using BeerSender.Web.Projections.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Add JSON options here
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterDomain();

builder.Services.AddDbContext<EventContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventStore"));
});

builder.Services.AddDbContext<ReadContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadStore"));
});

builder.Services.AddTransient<BoxStatusProjection>();
builder.Services.AddHostedService<ProjectionService<BoxStatusProjection>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapHub<EventHub>("/event-hub");

app.Run();
