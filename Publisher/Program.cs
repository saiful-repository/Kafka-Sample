using Confluent.Kafka;
using Publisher.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configures the Kafka producer with the localhost broker address.
var config = new ProducerConfig { 
    BootstrapServers = "localhost:9092"    
};

// Registers a singleton Kafka producer in the dependency injection container.
builder.Services.AddSingleton<IProducer<string, string>>(x =>
new ProducerBuilder<string, string>(config).Build());

//Register MessageProducer with a scoped lifetime.
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

