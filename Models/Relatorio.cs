using System.ComponentModel.DataAnnotations;

namespace JWForm.Models;

public class Relatorio
{
    public Relatorio()
    {
    }

    public Relatorio(DateTime data, int videos, int publicacoes, int revisitas, int estudosBiblicos, int horas, string observacao, int publicadorId)
    {
        Data = data;
        Videos = videos;
        Publicacoes = publicacoes;
        Revisitas = revisitas;
        EstudosBiblicos = estudosBiblicos;
        Horas = horas;
        Observacao = observacao;
        PublicadorId = publicadorId;
    }

    public void Atualizar(DateTime data, int videos, int publicacoes, int revisitas, int estudosBiblicos, int horas, string observacao, int publicadorId)
    {
        Data = data;
        Videos = videos;
        Publicacoes = publicacoes;
        Revisitas = revisitas;
        EstudosBiblicos = estudosBiblicos;
        Horas = horas;
        Observacao = observacao;
        PublicadorId = publicadorId;
    }

    public int RelatorioId { get; set; }

    [Required(ErrorMessage = "Selecione o mês")]
    [Display(Name = "Mês")]
    public DateTime Data { get; set; }

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

    [StringLength(600, ErrorMessage = "O texto está muito grande")]
    [Display(Name = "Observação")]
    public string Observacao { get; set; }

    public int PublicadorId { get; set; }
    public virtual Publicador Publicador { get; set; }
}