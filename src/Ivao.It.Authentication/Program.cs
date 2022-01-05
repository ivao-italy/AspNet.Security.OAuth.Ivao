using AspNet.Security.OAuth.Ivao;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = IvaoAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddIvao(opt =>
{
    opt.AuthorizationEndpoint = @"http://localhost/fakeivaologin/index.php";
    opt.TokenEndpoint = @"http://localhost/fakeivaologin/api.php?type=json&token=";
    opt.UserInformationEndpoint = @"http://localhost/fakeivaologin/api.php?type=json&token=";
});

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
          .WriteTo.Console()
          .WriteTo.Debug()
          .CreateLogger();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();



try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "App Crash");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
