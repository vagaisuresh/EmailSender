using EmailSender.Core.DIs;
using EmailSender.Core.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ModelToDtoProfile), typeof(ModelToDtoProfile));
builder.Services.RegisterSqlContext(builder.Configuration);
builder.Services.RegisterCoreServices();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
