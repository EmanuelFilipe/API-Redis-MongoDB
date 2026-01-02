using API.Infra;
using API.Mappers;
using API.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
});

#region [Database]

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

#endregion

#region [DI]

builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton<NewsService>();

#endregion

#region [AutoMapper]

builder.Services.AddAutoMapper(typeof(EntityToViewModelMapping), typeof(ViewModelToEntityMapping));

#endregion

#region [CORS]

builder.Services.AddCors();

#endregion

// torna o nome dos endpoints no swagger em minúsculo
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region [CORS]

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

#endregion

#region [StaticFiles]

app.UseStaticFiles();

// quem precisar de acessar a pasta 'Imagens' precisa ter '/imgs' na url
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Imagens")),
    RequestPath = "/imgs"
});

#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
