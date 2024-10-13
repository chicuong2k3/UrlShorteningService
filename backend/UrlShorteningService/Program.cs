using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using ShorteningService.Application.Behaviours;
using ShorteningService.Presentation.ExceptionHandlers;
using UrlShorteningService.Application.ShortLinks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region Logging
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

#endregion

#region Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion



#region EF & Authentication & Authorization
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
//.AddJwtBearer(x =>
//{
//    var secret = builder.Configuration["JwtAuthSettings:Secret"]!;
//    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
//    x.RequireHttpsMetadata = true;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        ValidAudience = builder.Configuration["JwtAuthSettings:Audience"]!,
//        ValidIssuer = builder.Configuration["JwtAuthSettings:Issuer"]!,
//        IssuerSigningKey = key,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };
//});



builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    //options.AddPolicy("User", policy => policy.RequireRole("User"));
});

#endregion

#region MediatR

List<Assembly> assemblies = [Assembly.GetExecutingAssembly()];

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblies.ToArray());

    config.AddOpenBehavior(typeof(ExceptionHandlingBehaviour<,>));
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(RequestLoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

builder.Services.AddSingleton<Stopwatch>();

#endregion

#region App Services

builder.Services.AddTransient<IShortUrlService, ShortUrlService>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
