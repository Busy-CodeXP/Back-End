using Buzy.DataAccess;
using dotnet.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;

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

        [HttpPost("distancia")]
        public IActionResult Distancia([FromBody]LocalizacaoViewModel model)
        {
            WebClient con = new WebClient();
            var origem = model.localizacao;
            var destino = model.destino;

            origem = string.Join("+", origem.ToString());
            destino = string.Join("+", destino.ToString());

            string.Format(Uri.EscapeDataString(origem), Uri.EscapeDataString(destino));

            var url = con.DownloadString($"https://maps.googleapis.com/maps/api/directions/json?mode=transit&transit_mode=bus&origin={origem}&destination={destino}&mode=driving&language=pt-BR&key=AIzaSyCXPixQua9NpsjwLDnhBnhe3qv-yzlv4z4");


            var result = JsonConvert.DeserializeObject(url);

            return Ok(result);
        }

    }
}