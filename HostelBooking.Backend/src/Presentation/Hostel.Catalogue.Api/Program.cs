using Hostel.Catalogue.Api.Extensions;
using Hostel.Catalogue.Application.Extensions;
using Hostel.Catalogue.Infrastructure.Dal.Extensions;
using Hostel.Catalogue.Infrastructure.Extensions;
using Hostel.Shared.Types.Logger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddDatabase();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddDefaultServices();

var app = builder.Build();

//app.MigrateDatabase(app.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
