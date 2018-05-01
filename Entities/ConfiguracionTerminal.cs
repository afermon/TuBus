namespace Entities
{
    public class ConfiguracionTerminal : BaseEntity
    {
        public int TerminalId { get; set; }
        public int CantidadQuejasSancion { get; set; }
        public int CantidadTardiasSancion { get; set; }
        public int CantidadMinutosTardia { get; set; }
        public double CostoParqueoHora { get; set; }
        public double CostoParqueoDia { get; set; }
        public double MontoInicialTarjeta { get; set; }
        public double CostoParqueoBusMes { get; set; }
    }
}
