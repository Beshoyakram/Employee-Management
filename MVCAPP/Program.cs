using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using MVCAPP.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* That's for enable Home controller to use interface class IEmployeeRepository with SQLEmployeeRepository */
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

#region //Access DB by connectionString in appSettings 
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDB")));
#endregion


#region //To apply all Identity user and rols tabels.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    /* options => to change and play in roles of password complexity.*/
    options => {
        options.Password.RequiredLength = 8;
        options.SignIn.RequireConfirmedEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); 
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();//for enable token for confim emails
#endregion

#region //For handel all Claims in roles with authorization.

builder.Services.AddAuthorization(options => {
    options.AddPolicy("CreateRolePolicy",
        policy => policy.RequireClaim("Create Role","true"));
    options.AddPolicy("EditRolePolicy",
        policy => policy.RequireClaim("Edit Role", "true"));
    options.AddPolicy("DeleteRolePolicy",
        policy => policy.RequireClaim("Delete Role", "true"));
});
#endregion


#region Google and facebook integration
builder.Services.AddAuthentication()
    
    .AddFacebook(facebookOptions =>
     {
         facebookOptions.ClientId = "1203736290251710";
         facebookOptions.ClientSecret = "69c62d9ec25de5a96847c4b8bede28ec";
     })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "664447212111-2u5464st4ighg0rp93fbohmpa002ruea.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    });
#endregion

#region Change token lifespan to 4 hours the default is 1 day
builder.Services.Configure<DataProtectionTokenProviderOptions>(
    option => option.TokenLifespan = TimeSpan.FromHours(4));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    /*If the user request wrong url redirct him into error controller*/
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
else 
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseRouting();


/*UseAuthentication */
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
