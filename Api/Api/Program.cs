using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;
// https://social.technet.microsoft.com/wiki/contents/articles/53073.asp-net-core-rest-api-documentation-using-swagger-c.aspx

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 1;
});
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//if(1==1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseRequestLocalization();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();

//api.infomanager.dk.DAL.Db.Day


// URL: https://www.ryadel.com/en/enable-or-disable-lazyloading-in-entity-framework/#:~:text=Disable%20Lazy%20Loading.%20In%20Entity%20Framework%204%20and,LazyLoadingEnabled%20property%20to%20false%20in%20the%20object%E2%80%99s%20constructor%3A