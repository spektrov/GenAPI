using GenApi.DomainServices;
using GenApi.Templates.Parser;
using GenApi.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi();
builder.Services.AddTemplateParser();
builder.Services.AddDomainServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
