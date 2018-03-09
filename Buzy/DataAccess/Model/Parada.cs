using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buzy.DataAccess.Model
{
    public class Parada
    {

        public int cp { get; set; }//Código de identificador da parada
        public string np { get; set; } //Nome da parada
        public string ed { get; set; } //Endereço de localização da parada
        public double py { get; set; } //Informação da latitude da localização da parada
        public double px { get; set; } //Informação da Longitude
    }
}
