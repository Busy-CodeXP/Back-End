using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buzy.DataAccess.Model
{
    public class LinhaLocalizada
    {
        public LinhaLocalizada()
        {

        }

        public String c { get; set; } //letreiro comleto
        public int cl{ get; set; } //Código identificador da linha
        public int sl { get; set; } //Sentido de operação onde 1 significa de Terminal Principal para Terminal Secundário e 2 de Terminal Secundário para Terminal Principal
        public string lt0 { get; set; } //Letreiro de destino da linha
        public string lt1 { get; set; } //Letreiro de origem da linha
        public int qv { get; set; } //Quantidade de veículos localizados
        public VeiculoAPI vs { get; set; }
    }
}
