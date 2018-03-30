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
        [DataMember(Name = "vs")]
        public List<VeiculoResult> vs{ get; set; }

        [DataMember(Name = "hr")]
        public string hr { get; set; }

    }

    [DataContract]
    public class VeiculoResult
    {
        [DataMember(Name = "p")]
        public int prefixo { get; set; }

        [DataMember(Name = "a")]
        public bool acessibilidade { get; set; }

        [DataMember(Name = "ta")]
        public string horarioDados { get; set; }

        [DataMember(Name = "py")]
        public double latitude { get; set; }

        [DataMember(Name = "px")]
        public double longitude { get; set; }

        [DataMember(Name = "capacidade")]
        public int capacidade { get; set; }

        [DataMember(Name = "lotacao")]
        public string lotacao { get; set; }
    
}
}
