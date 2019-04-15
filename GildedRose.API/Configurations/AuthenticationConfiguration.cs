using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GildedRose.Data;
using OpenIddict.Validation;

namespace GildedRose.API.Configurations
{
    public static class AuthenticationConfiguration
    {

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddOpenIddict()
            // Register the OpenIddict core services.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the EF Core stores/models.
                options.UseEntityFrameworkCore()
                   .UseDbContext<ApplicationDbContext>();
            })
            // Register the OpenIddict server handler.
            .AddServer(options =>
            {
                options.UseMvc();
                // Enable the token endpoint.
                options.EnableTokenEndpoint("/api/v1/auth/connect/token");
                // Enable the password flow.
                options.AllowPasswordFlow();
                // Accept anonymous clients (i.e clients that don't send a client_id).
                options.AcceptAnonymousClients();
                // only for demo purpose
                options.DisableHttpsRequirement();
            })
            // Register the OpenIddict validation handler.
            // Note: the OpenIddict validation handler is only compatible with the
            // default token format or with reference tokens and cannot be used with
            // JWT tokens. For JWT tokens, use the Microsoft JWT bearer handler.
            .AddValidation();
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = OpenIddictValidationDefaults.AuthenticationScheme;
                });

        }

    }
}
