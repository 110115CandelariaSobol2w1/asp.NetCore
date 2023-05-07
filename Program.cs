var builder = WebApplication.CreateBuilder(args);

var startUp = new startUp(builder.Configuration);
startUp.ConfigureServices(builder.Services);

var app = builder.Build();

startUp.Configure(app,app.Environment);

// Configure the HTTP request pipeline.


app.Run();
