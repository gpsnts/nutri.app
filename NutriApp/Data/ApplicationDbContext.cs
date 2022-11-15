using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriApp.Models;

namespace NutriApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<AlimentacaoDia> alimentacaoDias { get; set; }
    public DbSet<Food> foods { get; set; }
    public DbSet<FoodConsumida> foodConsumida { get; set; }

}
