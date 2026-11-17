

using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program);
string dbUrl = builder.Configuration.GetConnectionString("Database");

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly.Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly.Assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(dbUrl);
})
 .UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogData>();
}
builder.Services.AddHealthChecks()
                .AddNpgSql(dbUrl);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
