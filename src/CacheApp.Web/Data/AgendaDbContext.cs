using CacheApp.Web.Models;
using Microsoft.EntityFrameworkCore;
public class AgendaDbContext : DbContext
{
    public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
    {
    }

    public DbSet<Contato> Contatos
    { get; set; }
}
