using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheApp.Web.Data.Caching;

public abstract class BaseDistributedCache
{
    private readonly IDistributedCache _distributedCache;    

    protected BaseDistributedCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    protected void AtualizarCache(string key, object objeto, TimeSpan? tempo = null)
    {
        _distributedCache.SetString(key, JsonSerializer.Serialize(objeto), new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow =  tempo ?? TimeSpan.FromSeconds(30) });
    }

    protected TRet ObterRegistrosCache<TRet>(string key)
    {
        TRet retorno = default(TRet);

        var retornoCache = _distributedCache.GetString(key);
        if (!string.IsNullOrEmpty(retornoCache))
            retorno = JsonSerializer.Deserialize<TRet>(retornoCache);
        return retorno;
    }

    protected bool ExistemRegistros<TEnt>(IEnumerable<TEnt> lista)
    {
        return lista != null && lista.Count() > 0;
    }

}