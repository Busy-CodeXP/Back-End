using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buzy.DataAccess.Model
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string telefone { get; set; }
    }
}