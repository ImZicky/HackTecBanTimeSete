using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HackTecBanTimeSete.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HackTecBanTimeSete
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // JWT CONFIG

            //aqui vai nossa key secreta, o recomendado é guarda - la no arquivo de configuração
            var secretKey = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=DATTEBAYO";

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            //DEPENDENCY INJECTIONS
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();


            /* SWAGGER */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    //TODO: COLOCAR O NOSSO NOME AQUI
                    Title = "<<QD tivermos um nome  poe aqui hehe :P>>",
                    Description = "Uma Lista das APIS da <<QD tivermos um nome  poe aqui hehe :P>>",
                    TermsOfService = new Uri("https://google.com.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Contato",
                        Email = string.Empty,
                        Url = new Uri("https://google.com.br"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Nossa Licensa",
                        Url = new Uri("https://google.com.br"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Admin", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header usando um Bearer scheme. \r\n\r\n Digite 'Bearer' [espaço] e o seu token no input de texto abaixo.\r\n\r\nExemplo: \"Bearer 12345abcdefDatteBAYO\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Admin"                                
                            },

                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = "Admin",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Usuario/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuario}/{action=Index}/{id?}");
            });
        }
    }
}
