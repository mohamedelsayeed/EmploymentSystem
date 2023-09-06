
using EmploymentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Persistance.Context;

public class AppIdentityDBContext : IdentityDbContext<User,IdentityRole<int>,int>
{
    public AppIdentityDBContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Vacancy>()
                  .HasKey(v => v.Id);


        modelBuilder.Entity<Domain.Entities.Application>()
            .HasKey(a => a.Id);


        modelBuilder.Entity<Domain.Entities.Application>()
                    .HasOne(a => a.Vacancy)
                    .WithMany()
                    .HasForeignKey(v=>v.VacancyId)
                    .IsRequired();


    }


    public DbSet<User> Users { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Domain.Entities.Application> Applications { get; set; }


 
}
