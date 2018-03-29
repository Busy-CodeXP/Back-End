namespace Buzy.Controllers
{
    public class BusInfo
    {
        public string Identificacao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Acessivel { get; set; }
        public int Lotacao { get; set; }
    }
}