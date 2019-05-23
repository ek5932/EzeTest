namespace EzeTest.AuthServer
{
    using System;
    using System.Threading.Tasks;
    using AspNet.Security.OpenIdConnect.Primitives;
    using EzeTest.AuthServer.Model;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using OpenIddict.Abstractions;
    using OpenIddict.Core;
    using OpenIddict.EntityFrameworkCore.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // OAuth server registration
            services.AddOpenIddict()
                    .AddCore(options =>
                        options.UseEntityFrameworkCore()
                               .UseDbContext<ApplicationDbContext>())

                    // Register the OpenIddict server handler.
                    .AddServer(options =>
                    {
                        options.UseMvc();
                        options.EnableAuthorizationEndpoint("/connect/authorize")
                               .EnableLogoutEndpoint("/connect/logout")
                               .EnableIntrospectionEndpoint("/connect/introspect")
                               .EnableUserinfoEndpoint("/api/userinfo");

                        options.RegisterScopes(OpenIdConnectConstants.Scopes.Email);
                        options.AllowImplicitFlow();
                        options.EnableTokenEndpoint("/connect/token");
                        options.AllowClientCredentialsFlow();
                        options.DisableHttpsRequirement();
                        options.AddEphemeralSigningKey(); // TODO
                    })

                    // Register the OpenIddict validation handler.
                    .AddValidation();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();

            this.InitializeAsync(app.ApplicationServices).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();

                await CreateApplicationsAsync();
                await CreateScopesAsync();
                await CreateClients();

                async Task CreateApplicationsAsync()
                {
                    var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                    if (await manager.FindByClientIdAsync("aurelia") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "aurelia",
                            DisplayName = "Aurelia client application",
                            PostLogoutRedirectUris = { new Uri("http://localhost:9000/signout-oidc") },
                            RedirectUris = { new Uri("http://localhost:9000/signin-oidc") },
                            Permissions =
                            {
                                OpenIddictConstants.Permissions.Endpoints.Authorization,
                                OpenIddictConstants.Permissions.Endpoints.Logout,
                                OpenIddictConstants.Permissions.GrantTypes.Implicit,
                                OpenIddictConstants.Permissions.Scopes.Email,
                                OpenIddictConstants.Permissions.Scopes.Profile,
                                OpenIddictConstants.Permissions.Scopes.Roles,
                                OpenIddictConstants.Permissions.Prefixes.Scope + "api1",
                                OpenIddictConstants.Permissions.Prefixes.Scope + "api2"
                            }
                        };

                        await manager.CreateAsync(descriptor);
                    }

                    if (await manager.FindByClientIdAsync("resource-server-1") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "resource-server-1",
                            ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                            Permissions =
                            {
                                OpenIddictConstants.Permissions.Endpoints.Introspection
                            }
                        };

                        await manager.CreateAsync(descriptor);
                    }

                    if (await manager.FindByClientIdAsync("resource-server-2") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "resource-server-2",
                            ClientSecret = "C744604A-CD05-4092-9CF8-ECB7DC3499A2",
                            Permissions =
                            {
                                OpenIddictConstants.Permissions.Endpoints.Introspection
                            }
                        };

                        await manager.CreateAsync(descriptor);
                    }
                }

                async Task CreateScopesAsync()
                {
                    var manager = scope.ServiceProvider.GetRequiredService<OpenIddictScopeManager<OpenIddictScope>>();

                    if (await manager.FindByNameAsync("api1") == null)
                    {
                        var descriptor = new OpenIddictScopeDescriptor
                        {
                            Name = "api1",
                            Resources = { "resource-server-1" }
                        };

                        await manager.CreateAsync(descriptor);
                    }

                    if (await manager.FindByNameAsync("api2") == null)
                    {
                        var descriptor = new OpenIddictScopeDescriptor
                        {
                            Name = "api2",
                            Resources = { "resource-server-2" }
                        };

                        await manager.CreateAsync(descriptor);
                    }
                }

                async Task CreateClients()
                {
                    var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                    if (await manager.FindByClientIdAsync("console") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "console",
                            ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                            DisplayName = "My client application",
                            Permissions =
                            {
                                OpenIddictConstants.Permissions.Endpoints.Token,
                                OpenIddictConstants.Permissions.GrantTypes.ClientCredentials
                            }
                        };

                        await manager.CreateAsync(descriptor);
                    }
                }
            }
        }
    }
}
