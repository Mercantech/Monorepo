using Blazor.Components;
using Blazor.Service;

namespace Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            
            // Add QuizService
            builder.Services.AddScoped<IQuizService, QuizService>();
            
            // Add QuizIndexGenerator
            builder.Services.AddScoped<IQuizIndexGenerator, QuizIndexGenerator>();
            
            // Add Health Checks
            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();


            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
                
            // Map health check endpoint
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}
