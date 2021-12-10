using CacheApp.Web.Contracts;
using CacheApp.Web.Models;

namespace CacheApp.Web.Data.Repositories;
public class ContatoRepository : IContatoRepository
{
    private readonly AgendaDbContext _dbContext;

    public ContatoRepository(AgendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Contato> ListarTodos()
    {
        return _dbContext.Contatos.ToList();
    }

    public Contato? Obter(Guid id)
    {
        return _dbContext.Contatos.FirstOrDefault(f => f.Id == id);
    }

    public void Adicionar(Contato contato)
    {
        _dbContext.Add(contato);
        _dbContext.SaveChanges();
    }
}

