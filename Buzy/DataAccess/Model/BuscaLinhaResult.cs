using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Buzy.DataAccess.Model
{
    [DataContract]
    public class BuscaLinhaResult
    {
        [DataMember(Name = "cl")]
        public string codigoLinha { get; set; }

        [DataMember(Name = "lc")]
        public bool isCircular { get; set; }

        [DataMember(Name = "lt")]
        public string letreiro1 { get; set; }

        [DataMember(Name = "sl")]
        public int sentido { get; set; }

        [DataMember(Name = "tl")]
        public int letreiro2 { get; set; }

        [DataMember(Name = "tp")]
        public string terminalPrincipal { get; set; }

        [DataMember(Name = "ts")]
        public string terminalSecundario { get; set; }

        [DataMember(Name = "capacidade")]
        public int capacidade { get; set; }

        [DataMember(Name = "lotacao")]
        public string lotacao { get; set; }
    }
}
