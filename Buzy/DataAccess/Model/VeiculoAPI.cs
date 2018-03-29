using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Buzy.DataAccess.Model
{
    public class VeiculoAPI
    {
        public VeiculoAPI()
        {
        }

        public string p { get; set; } //Prefixo do veículo
        public bool a { get; set; }
        public double py { get; set; } //Informação de latitude da localização do veículo 
        public double px { get; set; } //Informação de longitude da localização do veículo
        public int lotacao { get; set; }

    }

}