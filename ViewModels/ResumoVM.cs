using JWForm.Models;

namespace JWForm.ViewModels;

public class ResumoVM
{
    public int totalDeHorasPublicadoresNaoBatizados {get; set; }

    public int totalDeHorasPublicadoresBatizados {get; set; }

    public int totalDeHorasPioneirosAuxiliares {get; set; }

    public int totalDeHorasPioneirosRegulares {get; set; }
    public int totalDeHoras {get; set; }
    
}