using Confluent.Kafka;
using WebConsumer.Kafka;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new ConsumerConfig()
{
    BootstrapServers = "localhost:9092",
    GroupId = "message_group_id",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

builder.Services.AddSingleton<IConsumer<string, string>>(x =>
new ConsumerBuilder<string, string>(config).Build());

builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();

builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
  .MinimumLevel.Debug()
 .WriteTo.File("logs/Message-.txt", rollingInterval: RollingInterval.Day)
 .CreateLogger();

app.Run();

