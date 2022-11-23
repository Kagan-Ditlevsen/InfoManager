using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
// https://social.technet.microsoft.com/wiki/contents/articles/53073.asp-net-core-rest-api-documentation-using-swagger-c.aspx

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
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