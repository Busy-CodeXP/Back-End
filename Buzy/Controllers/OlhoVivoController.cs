using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Buzy.DataAccess;
using Buzy.DataAccess.Model;
using Buzy.ViewModel;
using dotnet.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            request = new RestRequest("Linha/Buscar?termosBusca=", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpGet("buscaLinha")]
        public IActionResult BuscarLinhas(string buscaLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var termosBusca = buscaLinha;

            request = new RestRequest($"Linha/Buscar?termosBusca={termosBusca}", Method.GET);

            

            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;
                
            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpGet("codigoLinhaSensor")]
        public IActionResult codigoLinhaSensor(int codigoLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = codigoLinha;


            request = new RestRequest($"Posicao/Linha?codigoLinha={codLinha}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject<BuscaLinhaResult>(content);


            var prefixos = result.vs.Select(r => r.prefixo).ToArray();

            if (!_db.Sensores.Any(s => s.codigoLinha == codigoLinha)) throw new System.Exception();

            var historico = this._db.HistoricoSensores
                                .Where(hs => prefixos.Contains(hs.sensor.prefixo))
                                .Include(h => h.sensor).ToList();

            foreach(var item in result.vs)
            {
                var hists = historico.Where(h => h.sensor.prefixo == item.prefixo).ToList();

                var entradas = hists.Where(h => h.sensor.acao == AcaoSensor.Entrada).Count();
                var saidas = hists.Where(h => h.sensor.acao == AcaoSensor.Saida).Count();
                var total = entradas - saidas;
                var capacidade = 47;
                var lotacao = 0;

                if (total > 0)
                {
                    lotacao = (total * 100) / capacidade;
                }
                

                item.capacidade = capacidade;
                item.lotacao = string.Format("{0}", lotacao);
            }
            
            return Ok(result);
        }


        [HttpGet("buscaLinhaSentido")]
        public IActionResult buscaLinhaSentido(string buscaLinha, byte sentidos)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var termosBusca = buscaLinha;
            var sentido = sentidos; 

            request = new RestRequest($"Linha/BuscarLinhaSentido?termosBusca={termosBusca}&sentido={sentido}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        // API OLHO VIVO
        [HttpPost("buscaParada")]
        public IActionResult BuscarParada(string buscaParada)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse response = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = response.Content;

            var buscaParada_ = buscaParada;

            //o termo de busca é BuscaParada
            request = new RestRequest("Parada/Buscar?termosBusca={" + buscaParada_ + "}", Method.GET);
            response = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = response.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        //Posicao
        [HttpGet("Posicao")]
        public IActionResult PosicaoLinha(string codigoLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = codigoLinha;

            request = new RestRequest($"Posicao", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpGet("PosicaoLinha")]
        public IActionResult Posicao(int codigoLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = codigoLinha;

            request = new RestRequest($"Posicao/Linha?codigoLinha={codLinha}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpGet("PosicaoGaragem")]
        public IActionResult PosicaoGaragem(int codigoGaragem, int codigoLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = codigoLinha;
            var codGaragem = codigoGaragem;

            request = new RestRequest($"Posicao/Garagem?codigoGaragem={codGaragem}[&codigoLinha={codLinha}]", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        //Previsao
        [HttpGet("Previsao")]
        public IActionResult Previsao(string codigoParada, string codigoLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codParada = codigoParada;
            var codLinha = codigoLinha;

            request = new RestRequest($"Previsao?codigoParada={codParada}&codigoLinha={codLinha}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpGet("PrevisaoLinha")]
        public IActionResult PrevisaoLinha(string codigoLinha)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = codigoLinha;

            request = new RestRequest($"Previsao/Linha?codigoLinha={codLinha}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }

        [HttpGet("PrevisaoParada")]
        public IActionResult PrevisaoParada(string codigoParada)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codParada = codigoParada;

            request = new RestRequest($"Previsao/Parada?codigoParada={codParada}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }
    }

}