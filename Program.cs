using BlazorApp2.Components;
using BlazorApp2.Controllers;
using BlazorApp2.Data;
using BlazorApp2.Repository;
using BlazorApp2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MudBlazor.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log-.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true)
    .WriteTo.MSSqlServer(
        connectionString: "Server=localhost;Database=BlazorApp2;Integrated Security=SSPI;TrustServerCertificate=True;",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true, AutoCreateSqlDatabase = true})

    .CreateLogger();

builder.Services.AddSingleton(Log.Logger);
builder.Services.AddControllers();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductQueryService,ProductQueryService>();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BlazorApp2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorApp2Context") ?? throw new InvalidOperationException("Connection string 'BlazorApp2Context' not found.")));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "data/images")),
    RequestPath = "/files"
});
app.UseSwagger();
app.UseSwaggerUI();


try
{

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}
finally
{
    await Log.CloseAndFlushAsync(); // ensure all logs written before app exits
}
