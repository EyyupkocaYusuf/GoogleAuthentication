using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Kullan�c�lar�n kimlik do�rulama durumunu takip etmek i�in �erez tabanl� kimlik do�rulama kullan�l�r.
	options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // Varsay�lan kimlik do�rulamas� i�in google � kullan�r.
})
	.AddCookie()
	.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
	{
		options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value; //Google API'nin kimlik do�rulama isteklerini yaparken kullan�lacak istemci kimli�ini ayarlar. 
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
