using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Buzy.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Buzy.Controllers
{
    [Route("api/OlhoVivo")]
    public class BusyController : Controller
    {

        private BusHelperContext _db { get; set; }

        public BusyController(BusHelperContext db)
        {
            this._db = db;
        }

        public string BaseUrl
        {
            get
            {
                return "http://api.olhovivo.sptrans.com.br/v2.1";
            }
        }

    }
}