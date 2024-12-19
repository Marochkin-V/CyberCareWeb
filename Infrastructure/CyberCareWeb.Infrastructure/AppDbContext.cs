using Microsoft.EntityFrameworkCore;
using CyberCareWeb.Domain.Entities;

namespace CyberCareWeb.Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<ComponentType> ComponentTypes { get; set; }
	public DbSet<Component> Components { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderComponent> OrderComponents { get; set; }
}

