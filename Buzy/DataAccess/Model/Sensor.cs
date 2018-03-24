namespace Buzy.DataAccess.Model
{
    public class Sensor
    {
        public Sensor()
        {
        }

        public int Id { get; set; }
        public int valor { get; set; }
        public AcaoSensor acao { get; set; }
        public Veiculo VeiculoId { get; set; }
        
    }
}