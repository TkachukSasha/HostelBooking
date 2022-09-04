using Hostel.Security.Api.Extensions;
using Hostel.Security.Application.Extensions;
using Hostel.Security.Infrastructure.Dal.Extensions;
using Hostel.Security.Infrastructure.Extensions;
using Hostel.Shared.Types.Logger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDatabase();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
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
