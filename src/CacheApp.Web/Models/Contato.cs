using System.ComponentModel.DataAnnotations;

namespace CacheApp.Web.Models;
public class Contato
{
    public Contato()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Telefone { get; set; }
}

