using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Kullanýcýlarýn kimlik doðrulama durumunu takip etmek için çerez tabanlý kimlik doðrulama kullanýlýr.
	options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // Varsayýlan kimlik doðrulamasý için google ý kullanýr.
})
	.AddCookie()
	.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
	{
		options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value; //Google API'nin kimlik doðrulama isteklerini yaparken kullanýlacak istemci kimliðini ayarlar. 
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
	});

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
