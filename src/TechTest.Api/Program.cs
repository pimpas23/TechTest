using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TechTest.Api.Configuration;
using TechTest.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<MyDbContext>(options =>
{
    var x = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.ResolveDependencies();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Call Recording Details API",
        Description = "An ASP.NET Core Web API for managing Call Recording Details",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Bruno Pimparel",
            Url = new Uri("https://www.linkedin.com/in/bruno-gon%C3%A7alves-b988a271/")
        },
    });

    if(!builder.Environment.EnvironmentName.Equals("Testing"))
    {
        options.IncludeXmlComments("bin/TechTest.Api.xml");
        options.IncludeXmlComments("../TechTest.Business/bin/TechTest.Business.xml");
    }
    //options.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
