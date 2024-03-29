using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlite(connString);

    opt.EnableSensitiveDataLogging();
});
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BalanceService>();
builder.Services.AddScoped<ScenarioService>();
builder.Services.AddScoped<LoanDetailService>();
builder.Services.AddScoped<GeneralAssetCaseService>();
builder.Services.AddScoped<LoanRepaymentCaseService>();
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<JobSalaryService>();

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
