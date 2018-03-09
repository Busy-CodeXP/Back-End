using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buzy.DataAccess.Model
{
    public class Linha
    {
        public Linha() { }

        public int cl { get; set; } //Sentido de operação onde 1 significa de Terminal Principal para Terminal Secundário e 2 de Terminal Secundário para Terminal Principal
        public bool lc { get; set; } //Indica se uma linha opera no modo circular (sem um terminal secundário)
        public string lt { get; set; } //Informa a primeira parte do letreiro numérico da linha
        public int tl { get; set; } //Informa a segunda parte do letreiro numérico da linha, que indica se a linha opera nos modos: BASE (10), ATENDIMENTO (21, 23, 32, 41)
        public int sl { get; set; } //Informa o sentido ao qual a linha atende, onde 1 significa Terminal Principal para Terminal Secundário e 2 para Terminal Secundário para Terminal Principal
        public string tp { get; set; } //Informa o letreiro descritivo da linha no sentido Terminal Principal para Terminal Secundário
        public string ts { get; set; } //Informa o letreiro descritivo da linha no sentido Terminal Secundário para Terminal Principal

    }
}
