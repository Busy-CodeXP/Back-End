using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Buzy
{
    public class ApiService
    {
        public string BaseUrl
        {
            get
            {
                return "http://api.olhovivo.sptrans.com.br/v2.1";
            }
        }

        //public void GenerateToken(string code)
        //{
        //    string action = "Login/Autenticar?token=fb8530d792da1f8e5f60ebc0e14dead8ce7039ae86b533f2dbe80b9f8a2f162f";

        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + action);

        //    List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
        //    //parameters.Add(new KeyValuePair<string, string>(""))

        //    request.Content = new FormUrlEncodedContent()
        //}
    }
}
