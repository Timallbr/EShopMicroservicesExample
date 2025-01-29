var builder = WebApplication.CreateBuilder(args);


// Add services to container

//builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
//builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
//builder.Services.AddTransient<ILogger>(p =>
//{
//    var loggerFactory = p.GetRequiredService<ILoggerFactory>();
//    // You could also use the HttpContext to make the name dynamic for example
//    return loggerFactory.CreateLogger("my logger");
//});


builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    //options.AutoCreateSchemaObjects
}).UseLightweightSessions();



var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.Run();
