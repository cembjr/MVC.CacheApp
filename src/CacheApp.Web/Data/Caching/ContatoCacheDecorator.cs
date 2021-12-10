using CacheApp.Web.Contracts;
using CacheApp.Web.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheApp.Web.Data.Caching;
public class ContatoCacheDecorator<T> : BaseDistributedCache, IContatoRepository
    where T : IContatoRepository
{
    private readonly T _repository;
    private readonly ILogger<ContatoCacheDecorator<T>> _logger;

    private string _keyTodos() => "contato:todos";
    private string _keyObterContato(Guid id) => $"contato:{id.ToString("d")}";

    public ContatoCacheDecorator(IDistributedCache distributedCache, T repository, ILogger<ContatoCacheDecorator<T>> logger)
        : base(distributedCache)
    {
        _repository = repository;
        _logger = logger;
    }

    public void Adicionar(Contato contato)
    {
        _repository.Adicionar(contato);
        AtualizarListagemContatos();
    }

    public IEnumerable<Contato> ListarTodos()
    {
        var contatos = ObterRegistrosCache<IEnumerable<Contato>>(_keyTodos());

        if (!ExistemRegistros(contatos))
        {
            _logger.LogInformation($"Obtendo Registros do Banco. Key {_keyTodos()}");
            contatos = AtualizarListagemContatos();
        }
        else
            _logger.LogInformation($"Obtendo Registros do Cache. Key {_keyTodos()}");

        return contatos;
    }

    private IEnumerable<Contato> AtualizarListagemContatos()
    {
        IEnumerable<Contato> contatos = _repository.ListarTodos();
        AtualizarCache(_keyTodos(), contatos);
        return contatos;
    }

    public Contato? Obter(Guid id)
    {
        var key = _keyObterContato(id);
        var contato = ObterRegistrosCache<Contato>(key);

        if (contato == null)
        {
            _logger.LogInformation($"Obtendo Registros do Banco. Key {key}");
            contato = _repository.Obter(id);
            AtualizarCache(key, contato);
        }
        else
            _logger.LogInformation($"Obtendo Registros do Cache. Key {key}");

        return contato;
    }
}


public class ContatoCacheDecoratorScrutor : BaseDistributedCache, IContatoRepository
{
    private readonly IContatoRepository _repository;
    private readonly ILogger<ContatoCacheDecoratorScrutor> _logger;

    private string _keyTodos() => "contato:todos";
    private string _keyObterContato(Guid id) => $"contato:{id.ToString("d")}";

    public ContatoCacheDecoratorScrutor(IDistributedCache distributedCache, IContatoRepository repository, ILogger<ContatoCacheDecoratorScrutor> logger)
        : base(distributedCache)
    {
        _repository = repository;
        _logger = logger;
    }

    public void Adicionar(Contato contato)
    {
        _repository.Adicionar(contato);
        AtualizarListagemContatos();
    }

    public IEnumerable<Contato> ListarTodos()
    {
        var contatos = ObterRegistrosCache<IEnumerable<Contato>>(_keyTodos());

        if (!ExistemRegistros(contatos))
        {
            _logger.LogInformation($"Obtendo Registros do Banco. Key {_keyTodos()}");
            contatos = AtualizarListagemContatos();
        }
        else
            _logger.LogInformation($"Obtendo Registros do Cache. Key {_keyTodos()}");

        return contatos;
    }

    private IEnumerable<Contato> AtualizarListagemContatos()
    {
        IEnumerable<Contato> contatos = _repository.ListarTodos();
        AtualizarCache(_keyTodos(), contatos);
        return contatos;
    }

    public Contato? Obter(Guid id)
    {
        var key = _keyObterContato(id);
        var contato = ObterRegistrosCache<Contato>(key);

        if (contato == null)
        {
            _logger.LogInformation($"Obtendo Registros do Banco. Key {key}");
            contato = _repository.Obter(id);
            AtualizarCache(key, contato);
        }
        else
            _logger.LogInformation($"Obtendo Registros do Cache. Key {key}");

        return contato;
    }
}