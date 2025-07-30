using NotificationService.WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<NotificationQueueConsumer>();

var host = builder.Build();
host.Run();
