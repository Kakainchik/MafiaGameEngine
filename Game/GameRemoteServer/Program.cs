using GameRemoteServer;
using GameRemoteServer.Helpers;
using GameRemoteServer.Hubs;
using GameRemoteServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Configure DI strongly typed settings object
IConfigurationSection jwt = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwt);

//Controller services
builder.Services.AddControllers().AddNewtonsoftJson();

//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        SymmetricSecurityKey ssk = new(Encoding.ASCII.GetBytes(jwt["Secret"]));
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwt["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = ssk,
            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = delegate(MessageReceivedContext context)
            {
                var accessToken = context.Request.Query["accessToken"];
                
                //If the request is for hubs
                var path = context.HttpContext.Request.Path;
                if(!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/hubs"))
                {
                    //Read the token out of the query string
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

//Exception handler
builder.Services.AddExceptionHandler(options =>
{
    options.ExceptionHandler = async delegate(HttpContext context)
    {
        switch(context.Features.Get<IExceptionHandlerPathFeature>()?.Error)
        {
            case BadHttpRequestException ex:
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
                break;
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }
    };
});

//SignalR
builder.Services.AddSignalR();

//DB
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ServerContext>(options =>
{
    options.UseNpgsql(connection);
});

//Custom services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddSingleton<IHallService, PublicHallService>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Middlewares
app.UseExceptionHandler();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");
app.MapHub<LobbyHub>("/hubs/lobby");

app.Run();