using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiToProject.Entities;
using ApiToProject.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace ApiToProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DataBaseContext>(opt =>
            //    opt.UseInMemoryDatabase("EmployeeList"));
            services.AddMvc();

            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=DbProfileGenerator;Trusted_Connection=True;";
            services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(connectionString));

            //register repo
            services.AddScoped<IProfileGeneratorRepository, ProfileGeneratorRepository> ();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataBaseContext dataBaseContext)
        {
            

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Employee, Models.EmployeeDto>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                       $"{src.FirstName} {src.LastName} {src.Specialization} {src.YearsOfWork}"));

                cfg.CreateMap<Models.EmployeeForCreationDto, Entities.Employee>();
            });

            dataBaseContext.SeedDataForContext();

            app.UseMvc();
        }
    }
}