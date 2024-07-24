using Application;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Configurations;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);


builder.Services.AddApplicationServices(
    fileLogConfiguration: builder
        .Configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
        .Get<FileLogConfiguration>()
        ?? throw new InvalidOperationException("FileLogConfiguration section cannot found in configuration.")
);

builder.Services.AddLogging();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddAntiforgery(opt => opt.Cookie.Name = "X-CSRF-TOKEN");

builder.Services.AddWebOptimizer();



builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "BlogZ";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseWebOptimizer();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Home/PageNotFound", "?statusCode={0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

