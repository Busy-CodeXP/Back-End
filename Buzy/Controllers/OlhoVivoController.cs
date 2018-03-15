﻿using System.Collections.Generic;
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
        [HttpGet("buscaLinhaSentido")]
        public IActionResult buscaLinhaSentido(string buscaLinha, byte sentido)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var termosBusca = buscaLinha;

            return null;
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
        public IActionResult Posicao(string codigoLinha)
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

        //Previsao
        [HttpGet("Previsao")]
        public IActionResult Previsao(string codigoLinha, string codigoParada)
        {
            RestClient restClient = new RestClient("http://api.olhovivo.sptrans.com.br/v2.1")
            {
                CookieContainer = new CookieContainer()
            };

            RestRequest request = new RestRequest("Login/Autenticar?token=6f76933e898283a0bbf03b5aa3ee0a4e22f7b8dcb47abfeef4cd9f4300690a92", Method.POST);
            RestResponse resp = (RestResponse)restClient.ExecuteAsPost(request, "POST");
            var content = resp.Content;

            var codLinha = codigoLinha;
            var codParada = codigoParada;

            request = new RestRequest($"Previsao/Linha?codigoParada={codParada}&codigoLinha={codLinha}", Method.GET);
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

            request = new RestRequest($"Previsao/Linha?codigoParada={codParada}", Method.GET);
            resp = (RestResponse)restClient.ExecuteAsGet(request, "GET");
            content = resp.Content;

            var result = JsonConvert.DeserializeObject(content);

            return Ok(result);
        }






    }
}