using System.Collections.Generic;
using System.Net;
using Buzy.DataAccess;
using Buzy.ViewModel;
using dotnet.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Buzy.Controllers
{
    [Produces("application/json")]
    [Route("api/OlhoVivo")]
    public class OlhoVivoController : Controller
    {
        private BusHelperContext _db { get; set; }

        public OlhoVivoController(BusHelperContext db)
        {
            this._db = db;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=fb8530d792da1f8e5f60ebc0e14dead8ce7039ae86b533f2dbe80b9f8a2f162f", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            request = new RestRequest("Linha/Buscar?termosBusca=8000", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult BuscarLinhas([FromBody]BuscaLinhasViewModel linhasViewModel)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var termosBusca = linhasViewModel.termosBusca;

            termosBusca = string.Join("+", termosBusca.ToString());

            request = new RestRequest($"Linha/Buscar?termosBusca={termosBusca}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        // API OLHO VIVO
        [HttpPost("buscaParada")]
        public IActionResult BuscarParada([FromBody]BuscaParadaViewModel viewModel)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse response = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = response.Content;

            var busca = viewModel.buscaParada;

            request = new RestRequest("Parada/Buscar?termosBusca={" + busca + "}", Method.GET);
            response = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = response.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpPost("Posicao")]
        public IActionResult Posicao([FromBody]PosicaoViewModel viewModel)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = viewModel.codigoLinha;

            request = new RestRequest($"Posicao/Linha?codigoLinha={codLinha}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpPost("Previsao")]
        public IActionResult Previsao([FromBody]PrevisaoChegadaViewModel viewModel)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = viewModel.codigoLinha;

            request = new RestRequest($"Previsao/Linha?codigoLinha={codLinha}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }


        //[HttpPost]
        //public IActionResult ConsultaApi([FromBody]BuscaLinhasViewModel linhasViewModel, [FromBody]LocalizacaoViewModel localizacaoViewModel)
        //{
        //    RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
        //    {
        //        CookieContainer = new CookieContainer()
        //    };

        //    RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
        //    RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
        //    var content = resp.Content;

        //    // Recebe o valor localizacaoViewModel
        //    var origem = localizacaoViewModel.localizacao;
        //    var destino = localizacaoViewModel.destino;

        //    var termoBusca = linhasViewModel.termosBusca;

        //    termoBusca = string.Join("+", termoBusca.ToString());

        //    request = new RestRequest($"Linha/Buscar?termosBusca={termoBusca}", Method.GET);
        //    resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
        //    content = resp.Content;

        //    var result = JsonConvert.DeserializeObject(content);

        //    return Ok(result);
        //}



    }
}