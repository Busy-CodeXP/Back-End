using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Buzy.DataAccess;
using dotnet.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Buzy.Controllers
{
    [Produces("application/json")]
    [Route("api/Localizacao")]
    public class LocalizacaoController : Controller
    {
        private BusHelperContext _db { get; set; }

        public LocalizacaoController(BusHelperContext db)
        {
            this._db = db;
        }

        [HttpGet("distancia")]
        public IActionResult Distancia([FromBody]LocalizacaoViewModel model)
        {
            WebClient con = new WebClient();
            var origem = model.localizacao;
            var destino = model.destino;

            origem = string.Join("+", origem.ToString());
            destino = string.Join("+", destino.ToString());

            var url = con.DownloadString($"https://maps.googleapis.com/maps/api/directions/json?units=metric&origin={origem}&destination={destino}&mode=bicycling&language=pt-BR&key=AIzaSyCXPixQua9NpsjwLDnhBnhe3qv-yzlv4z4");

            var result = JsonConvert.DeserializeObject(url);

            return Ok(result);
        }

    }
}