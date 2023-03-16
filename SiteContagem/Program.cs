using SiteContagem.Data;
using Microsoft.ApplicationInsights.DependencyCollector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ContagemRepository>();

builder.Services.AddRazorPages();

var connectionAppInsights = builder.Configuration.GetConnectionString("ApplicationInsights");
if (!string.IsNullOrWhiteSpace(connectionAppInsights))
{
    builder.Services.AddApplicationInsightsTelemetry(options =>
    {
        options.ConnectionString = connectionAppInsights;
    });

    builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
    {
        module.EnableSqlCommandTextInstrumentation = true;
    });
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();