using EmploymentSystem.Services;
using EmploymentSystem.WebApi.Extentions;
using Quartz.Impl;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddApplictaionServices(builder.Configuration);



var app = builder.Build();


try
{
    ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

    // Get a scheduler
    IScheduler scheduler = await schedulerFactory.GetScheduler();

    // Start the scheduler
    await scheduler.Start();

    // Define a job and trigger
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

    // Schedule the job
    await scheduler.ScheduleJob(job, trigger);
}
catch (Exception)
{

    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
