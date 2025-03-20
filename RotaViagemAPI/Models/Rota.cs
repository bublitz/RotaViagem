namespace RotaViagemAPI.Models
{
    public class Rota
    {
        public required int Id { get; set; }
        public required string Origem { get; set; }
        public required string Destino { get; set; }
        public int Valor { get; set; }
    }
}
