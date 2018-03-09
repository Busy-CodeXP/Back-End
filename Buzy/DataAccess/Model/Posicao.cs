using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buzy.DataAccess.Model
{
    public class Posicao
    {
        public String hr { get; set; } //Horario de referencia da geração das informações
        public LinhaLocalizada[] l { get; set; } //Relação de linhas localizadas
    }
}
