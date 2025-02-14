using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using forms.Components;
using forms.Components.Account;
using forms.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// builder.Services.AddRazorPages()
//     .AddMicrosoftIdentityUI();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddMicrosoftAccount(microsoftOptions =>
   {
       microsoftOptions.ClientId = builder.Configuration.GetValue<string>("AzureAd:ClientId")!;
       microsoftOptions.ClientSecret = builder.Configuration.GetValue<string>("AzureAd:ClientSecret")!;
       microsoftOptions.CallbackPath = "/signin-microsoft";
   })
   .AddTwitter(twitterOptions =>
    {
        twitterOptions.ConsumerKey = builder.Configuration.GetValue<string>("Twitter:ApiKey")!;
        twitterOptions.ConsumerSecret = builder.Configuration.GetValue<string>("Twitter:ApiSecret")!;
    })
    .AddIdentityCookies();


// builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
// .EnableTokenAcquisitionToCallDownstreamApi()
// .AddInMemoryTokenCaches();

// builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
// {
//     options.Events.OnRedirectToIdentityProviderForSignOut = (context) =>
//     {
//         var logoutUri = builder.Configuration["AzureAd:SignedOutCallbackPath"] ?? "/";
//         context.ProtocolMessage.PostLogoutRedirectUri = context.Request.Scheme + "://" + context.Request.Host + logoutUri;
//         return Task.CompletedTask;
//     };
// });


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddSingleton<MinioService>();

builder.Services.AddSingleton<LinkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();


// app.MapControllers(); // Ensures Microsoft Identity UI endpoints exist
// app.MapRazorPages();  // Enables authentication UI pages

app.Run();
