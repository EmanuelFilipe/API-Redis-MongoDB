using API.Infra;
using API.Mappers;
using API.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            },
            new string[] {}
        }
    });
});

#region [Database]

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

#endregion

#region [HealthCheck]

builder.Services.AddHealthChecks()
                .AddMongoDb(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value + "/" + builder.Configuration.GetSection("DatabaseSettings:DatabaseName").Value,
                    name: "mongodb", tags: new string[] { "db", "data" });

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15); // time between checks
    opt.MaximumHistoryEntriesPerEndpoint(60); // maximum history of checks
    opt.SetApiMaxActiveRequests(1); // api requests concurrency
    opt.AddHealthCheckEndpoint("default api", "/health"); // map health check endpoint
}).AddInMemoryStorage(); // use in-memory storage for health check history

#endregion

#region [DI]

builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton<NewsService>();
builder.Services.AddSingleton<VideoService>();
builder.Services.AddTransient<UploadService>();
builder.Services.AddTransient<GalleryService>();

#endregion

#region [AutoMapper]

builder.Services.AddAutoMapper(typeof(EntityToViewModelMapping), typeof(ViewModelToEntityMapping));

#endregion

#region [CORS]

builder.Services.AddCors();

#endregion

#region [JWT]
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(builder.Configuration.GetSection("tokenManagement:secret").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
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


#region [HealthCheck]

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).UseHealthChecksUI(h => h.UIPath = "/health-ui");

#endregion

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
        Path.Combine(builder.Environment.ContentRootPath, "Medias")),
    RequestPath = "/medias"
});

#endregion

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
