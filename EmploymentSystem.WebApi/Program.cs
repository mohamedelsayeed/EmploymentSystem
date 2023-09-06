using EmploymentSystem.Services;
using EmploymentSystem.WebApi.Extentions;
using Quartz.Impl;
using Quartz;
using EmploymentSystem.Application.Authentication;
using Microsoft.AspNetCore.Identity;
using Serilog;
using EmploymentSystem.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var jwtSetting = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSetting);
builder.Services.AddIdentityServices(builder.Configuration, jwtSetting);

builder.Services.AddApplictaionServices(builder.Configuration);



var app = builder.Build();


try
{
    ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

    IScheduler scheduler = await schedulerFactory.GetScheduler();

    await scheduler.Start();

    IJobDetail job = JobBuilder.Create<VacanciesArchievingJobService>()
        .WithIdentity("myJob", "myGroup")
        .Build();

    ITrigger trigger = TriggerBuilder.Create()
        .WithIdentity("myTrigger", "myGroup")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(24)
            .RepeatForever())
        .Build();

    await scheduler.ScheduleJob(job, trigger);
}
catch (Exception)
{

    throw;
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

await roleManager.CreateAsync(new IdentityRole<int>("Employer"));
await roleManager.CreateAsync(new IdentityRole<int>("Applicant"));


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
