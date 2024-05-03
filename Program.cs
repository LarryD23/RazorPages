using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;
using Microsoft.Extensions.Caching.Distributed;
using Vote_Final;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddScoped<TwilioVerifyService>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();




builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.AddDbContext<DomainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    
}


//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();



