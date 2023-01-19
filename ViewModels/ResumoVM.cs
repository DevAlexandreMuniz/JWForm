using JWForm.Models;

namespace JWForm.ViewModels;

public class ResumoVM
{
    public IEnumerable<Publicador> totalDeHorasPublicadoresNaoBatizado {get; set; }

    public IEnumerable<Publicador> totalDeHorasPublicadoresBatizado {get; set; }

    public IEnumerable<Publicador> totalDeHorasPioneiroAuxiliar {get; set; }

    public IEnumerable<Publicador> totalDeHorasPioneiroRegular {get; set; }
    public int totalDeHoras {get; set; }
    
}