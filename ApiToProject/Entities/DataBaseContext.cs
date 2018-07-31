using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiToProject.Entities;

namespace ApiToProject.Entities
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Language> Languages { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Many to many Employee to Project


            modelBuilder.Entity<EmployeeProject>()
                .HasIndex(u => new { u.ProjectId, u.EmployeeId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(pc => pc.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(pe => pe.Employee)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(pc => pc.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            //---------------------------------------------

            modelBuilder.Entity<EmployeeLanguage>()
                .HasIndex(u => new { u.EmployeeId, u.LanguageId });

            modelBuilder.Entity<EmployeeLanguage>()
                .HasOne(a => a.Employee)
                .WithMany(a => a.EmployeeLanguages)
                .HasForeignKey(a => a.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeLanguage>()
                .HasOne(pe => pe.Language)
                .WithMany(p => p.EmployeeLanguages)
                .HasForeignKey(pc => pc.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            //------------------------------------------------------

            modelBuilder.Entity<EmployeeSkill>()
               .HasIndex(u => new { u.EmployeeId, u.SkillId });

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(x => x.Employee)
                .WithMany(p => p.EmployeeSkills)
                .HasForeignKey(pc => pc.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(x => x.Skill)
                .WithMany(p => p.EmployeeSkills)
                .HasForeignKey(pc => pc.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


        }



    }
}