using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connecctionstring = builder.Configuration.GetConnectionString("DbConnection");
ReactWebApplication.Class.Users.DSOURCE = connecctionstring;
//builder.Services.AddDbContext<AppDBContext>(Options => Options.UseMySQL(connecctionstring));
builder.Services.AddDbContext<AppDBContext>(Options => Options.UseMySql(connecctionstring, ServerVersion.AutoDetect(connecctionstring)));
#region
//builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion
builder.Services.AddControllers().AddNewtonsoftJson();//(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options =>options.SerializerSettings.ContractResolver = new DefaultContractResolver());
//IServiceCollection serviceCollection = builder.Services.AddTransient<IRespositroy,Respository>();
//builder.Services.AddCors(options =>
//{
//Specific Domain only....
//    options.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//});



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("api", new OpenApiInfo { Title = "Anugraha Fashion Mill Private Limited -API", Description = "React Web Application", Version = "v1" });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o=>o.SwaggerEndpoint("api/swagger.json", "React Web Application"));
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/api/v1/swagger.json", "ReactWebApi v1"));
}
//app.UseCors(x=>x.WithOrigins("CustomPolicy").AllowAnyHeader().AllowAnyMethod());
app.UseCors(x =>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
