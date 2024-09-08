using API.Injector;
using BLL.Authorization;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Settings;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// .....................................................................................
// ............................................................ Inject Services
// ........................................ Starting

APIInjector.APIConfigureServices(builder.Services, builder.Configuration);




// .....................................................................................
// ............................................................ Inject Services
// ........................................ End



// .....................................................................................
// ............................................................ CORS
// ........................................ Starting

builder.Services.AddCors(options => {
    options.AddPolicy(name: "AngularClient",
        builder => {
            builder.WithOrigins(AppSettings.ClientOrigin).AllowAnyHeader().AllowAnyMethod();
        });
});

// .....................................................................................
// ............................................................ CORS
// ........................................ End



// .....................................................................................
// ................................................ Policy Based API Authorization
// ........................................ Starting

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin")
              .RequireClaim("AdminClaim", "true") // User claim
              .AddRequirements(new RoleClaimRequirement("Admin", "AdminRoleClaim", "true")) // Role claim
              .AddRequirements(new RoleClaimRequirement("Admin", "AnotherAdminRoleClaim", "value"))); // Additional role claim

    options.AddPolicy("RequireSystemAdminRole", policy =>
        policy.RequireRole("System Admin")
              .RequireClaim("SystemAdminClaim", "true") // User Claim
              .AddRequirements(new RoleClaimRequirement("System Admin", "SystemAdminRoleClaim", "true")) // Role claim
              .AddRequirements(new RoleClaimRequirement("System Admin", "AnotherSystemAdminRoleClaim", "value"))); // Additional role claim
});


// .....................................................................................
// ................................................ Policy Based API Authorization
// ........................................ End



// .....................................................................................
// ................................................. JWT Token Create & Validation
// ........................................ Starting


// ................................................... Token Create

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, // Validate with Token Expiration Time
        ValidIssuer = AppSettings.ApiValidIssuer,
        ValidAudience = AppSettings.ApiValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.SymmetricSecurityKey))
    };


    // ............................................... Token Validation

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            // Access the HTTP context accessor to retrieve the current HTTP context
            var httpContextAccessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();

            // Retrieve the authorization header containing the JWT token
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            // Extract the JWT token from the authorization header, removing the "Bearer " prefix
            var activeToken = authorizationHeader?.Replace("Bearer ", "");

            // Retrieve the current user agent from the request headers
            var currentUserAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            // Retrieve the 'UserData' claim from the validated token
            var userAgentClaim = context.Principal.FindFirst(ClaimTypes.UserData)?.Value;

            // Validate if the user agent in the claim matches the current user agent
            if (userAgentClaim != currentUserAgent)
            {
                context.Fail("Token is not valid for this user agent.");
                return;
            }


            // Extract the username from the 'Name' claim in the token
            string usernameClaim = context.Principal.FindFirst(ClaimTypes.Name)?.Value;

            // Retrieve logout time and access status from the database for the user
            using (var scope = httpContextAccessor.HttpContext.RequestServices.CreateScope())
            {
                // Obtain an instance of the DbContext from the scoped service provider
                var dbContext = scope.ServiceProvider.GetRequiredService<ControlPanelDbContext>();

                // Call a method to retrieve logout time and access status for the user
                var logoutAndAccess = await dbContext.GetLogoutTimeAndAccessForUserAsync(usernameClaim, currentUserAgent, activeToken);

                // Check if access is denied based on the retrieved information
                if (!logoutAndAccess.access)
                {
                    context.Fail("Invalid token.");
                    return;
                }

                // Check if the token is expired due to logout
                if (logoutAndAccess.logout)
                {
                    context.Fail("Token is expired due to logout.");
                    return;
                }
            }

            // Token validation successful
            await Task.CompletedTask;
        }
    };

});




// ......................................... Authorize Button
// ......................................... Add authentication to Swagger UI

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ReCom Consulting Limited", Version = "RCL ERP v2" });

    options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT"
                }
            },
            new string[] { }
        }
    });
});


// .....................................................................................
// ................................................. JWT Token Create & Validation
// ........................................ End



var app = builder.Build();



// .....................................................................................
// ...................................................... HTTPContextAccessor
// ........................................ Starting



var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
var serviceProvider = app.Services;

// User Helper
UserHelper.Configure(httpContextAccessor, () => serviceProvider.CreateScope());

// Activiry Log
StoreActivityLogger<ControlPanelDbContext>.Configure(httpContextAccessor);




// .....................................................................................
// ...................................................... HTTPContextAccessor
// ........................................ End

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RCL ERP v2");

        // Inject the custom CSS for smaller padding and margin
        c.InjectStylesheet("/swagger-ui/custom.css");

        // Other UI customizations
        c.ConfigObject.DocExpansion = Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None;
        c.DocumentTitle = "RCL ERP API Documentation";

        c.HeadContent = @"
        <style>
            body {
                font-size: 12px;
                background-color: #f5f5f5;
                padding: 0;
                margin: 0;
            }
            .swagger-ui .topbar {
                background-color: #333;
            }

            .swagger-ui .scheme-container {
                background: inherit;
                box-shadow: none;
                margin: 0 0 5px;
                padding: 0;
            }

            .swagger-ui .info {
                margin: 20px 0 0 0;
            }

            .swagger-ui .info hgroup.main {
                margin: 0 0 0px;
            }

            .swagger-ui .opblock-tag {
                color: #3b4151;
                font-family: sans-serif;
                font-size: 18px;
                margin: 0 0 2px;
            }
            .swagger-ui .opblock-tag {
                align-items: center;
                border-bottom: 1px solid rgba(59, 65, 81, .3);
                cursor: pointer;
                display: flex;
                padding: 10px;
                transition: all .2s;
            }

            .swagger-ui .expand-methods svg, .swagger-ui .expand-operation svg {
                height: 15px;
                width: 15px;
            }

            .swagger-ui .opblock {
                box-shadow: 0 0 1px rgba(0, 0, 0, .1);
                margin: 0 0 5px;
            }

            .swagger-ui .opblock .opblock-summary-method {
                font-size: 12px;
            }

            .swagger-ui .opblock .opblock-summary-operation-id, .swagger-ui .opblock .opblock-summary-path, .swagger-ui .opblock .opblock-summary-path__deprecated {
                font-size: 12px;
                padding: 5px;
            }

            svg.arrow {
                height: 15px;
                width: 15px;
            }

            svg {
                height: 15px;
                width: 15px;
            }

            .swagger-ui .btn.authorize span {
                padding: 1px 20px 0 0;
            }

            .swagger-ui .opblock-tag {
                border-bottom: none;
                box-shadow: 0px 1px 1px rgba(0, 0, 0, 0.2); 
                border-radius: 4px; 

            }
            .swagger-ui section h3 {
                color: #3b4151;
                font-family: sans-serif;
            }

            .swagger-ui .scheme-container .schemes {
                font-size: 12px;
                color: #333333;
            }
            .swagger-ui .topbar .wrapper .title span,
            .swagger-ui .topbar .wrapper .info .title,
            .swagger-ui .topbar .wrapper .info .description p,
            .swagger-ui .topbar .wrapper .info .version,
            .swagger-ui .topbar .wrapper .info .contact,
            .swagger-ui .topbar .wrapper .info .license,
            .swagger-ui .info .title h1,
            .swagger-ui .info .version p,
            .swagger-ui .info .contact h4,
            .swagger-ui .info .license h4,
            .swagger-ui .opblock-summary-description p,
            .swagger-ui .opblock-section-header,
            .swagger-ui .opblock .opblock-section .opblock-section-header h4,
            .swagger-ui .opblock .opblock-body .opblock-body pre,
            .swagger-ui .opblock .opblock-body .opblock-media-type,
            .swagger-ui .opblock .opblock-request .opblock-request-body pre,
            .swagger-ui .opblock .opblock-request .opblock-request-body .opblock-media-type,
            .swagger-ui .opblock .opblock-response .opblock-response-body pre,
            .swagger-ui .opblock .opblock-response .opblock-response-body .opblock-media-type,
            .swagger-ui .footer,
            .swagger-ui .info .description p {
                font-size: 12px;
                color: #555555;
            }
            .swagger-ui .wrapper .info .title {
                color: #3b5998;
                font-size: 24px;
            }
        </style>
    ";
    });
}
    // ..................................................................
    // ............................................. UI Design
    // .................................. End







// ..................................................................
// ............................................. Uses
// .................................. Starting

// ............................. Cors
app.UseCors("AngularClient");


// ............................. Authentication
app.UseAuthentication();


// ..................................................................
// ............................................. Uses
// .................................. End


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
