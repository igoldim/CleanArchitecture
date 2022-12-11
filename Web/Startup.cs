using Application.Behaviors;
using MediatR;
using FluentValidation;
using Microsoft.OpenApi.Models;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
            services.AddControllers().AddApplicationPart(presentationAssembly);

            var applicationAssembly = typeof(Application.AssemblyReference).Assembly;
            services.AddControllers().AddApplicationPart(applicationAssembly);

            services.AddMediatR(applicationAssembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddSwaggerGen(c =>
            {
                var presentationDocumentationFile = $"{presentationAssembly.GetName()}";

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceApi", Version = "v1" });
            });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceApi v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
        }
    }
}
