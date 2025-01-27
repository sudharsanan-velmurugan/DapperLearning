using DapperLearning.Exception_Handler;
using DapperLearning.Repository;
using DapperLearning.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    //options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<SwaggerDocumentFilter>();
});

var app = builder.Build();
var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach(var desc in versionDescProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",$"Employee {desc.GroupName.ToUpper()}");
        }
    });
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
    //    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Employee API V2");
    //});

}
app.UseExceptionHandler(o => { });
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
