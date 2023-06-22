using System.ComponentModel;

public enum TipoPublicador
{
    [Description("Não Batizado")]
    NaoBatizado,

    [Description("Batizado")]
    Batizado,

    [Description("Pionerio Auxiliar")]
    PioneiroAuxiliar,
    
    [Description("Pioneiro Regular")]
    PioneiroRegular
}