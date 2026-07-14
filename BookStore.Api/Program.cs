using BookStore.Api;
using Hangfire;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/Jobs", new DashboardOptions
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
           User = app.Configuration.GetValue<string>("HangfireSettings:UserName"),
           Pass = app.Configuration["HangfireSettings:Password"]
        }
    ],
    DashboardTitle = "BookStore Jobs",
    IsReadOnlyFunc = (context) => true
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
