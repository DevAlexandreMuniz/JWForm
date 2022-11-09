using System.ComponentModel.DataAnnotations;

namespace JWForm.Models;

public class Relatorio
{
    public int RelatorioId { get; set; }

    [StringLength(50, ErrorMessage = "O tamanho máximo é 50 caracteres")]
    [Required(ErrorMessage = "Selecione o mês")]
    [Display(Name = "Mês")]
    public string Mes { get; set; }

    [Display(Name = "Vídeos")]
    public int Videos { get; set; }

    [Display(Name = "Publicações")]
    public int Publicacoes { get; set; }

    [Display(Name = "Revisitas")]
    public int Revisitas { get; set; }

    [Display(Name = "Estudos Bíblicos")]
    public int EstudosBiblicos { get; set; }

    [Required(ErrorMessage = "Informe o total de horas")]
    [Display(Name = "Horas")]
    public int Horas { get; set; }

    [StringLength(50, ErrorMessage = "O tamanho máximo é 100 caracteres")]
    [Display(Name = "Observação")]
    public string Observacao { get; set; }

    public int PublicadorId { get; set; }
    public virtual Publicador Publicador { get; set; }
}