using CacheApp.Web.Models;

namespace CacheApp.Web.Contracts;
public interface IContatoRepository
{
    IEnumerable<Contato> ListarTodos();
    Contato? Obter(Guid id);

    void Adicionar(Contato contato);
}
