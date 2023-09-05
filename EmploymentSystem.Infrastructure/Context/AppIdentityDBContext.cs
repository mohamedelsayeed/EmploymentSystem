
using EmploymentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Persistance.Context;

public class AppIdentityDBContext : IdentityDbContext
{
    public AppIdentityDBContext(DbContextOptions options) : base(options)
    {
         
    }
    public DbSet<User> Users { get; set; }
}
