using System.ComponentModel.DataAnnotations;

namespace JWForm.Models;

public class Publicador
{
    public int PublicadorId { get; set; }

    [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
    [Required(ErrorMessage = "Escreva o nome do publicador")]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
    [Required(ErrorMessage = "Escreva o nome do grupo de campo")]
    [Display(Name = "Nome")]
    public string GrupoDeCampo { get; set; }
    
    public bool EnviouORelatorio { get; set; }

    public List<Relatorio> Relatorios { get; set; }
}