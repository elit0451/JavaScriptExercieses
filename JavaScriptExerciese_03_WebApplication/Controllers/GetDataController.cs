using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JavaScriptExerciese_03_WebApplication.Controllers
{
    public class GetDataController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetContentDataFromUri(string uri)
        {
            if (uri == null || uri.Length == 0)
                return GetDefault();
            // e.g.  uri = "http://bjoerks.net/bjbunetindex.htm";
            // e.g.  uri = "https://www.dr.dk/Nyheder/Service/feeds/regionale/fyn/#";
            // e.g.  uri = "/ExampleData/Personer.json";
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                string responseText = client.DownloadString(uri);
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(responseText);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
                response.Content.Headers.Add("access-control-allow-origin", "*");
                response.Content.Headers.Add("access-control-allow-methods", "GET");
                return response;
            }
            catch (Exception e)
            {
                string responseText = "error: "+e.Message;
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent(responseText);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
                return response;
            }

        }
        [HttpGet]
        public HttpResponseMessage GetDefault()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            string responseText = "syntax is: " + Environment.NewLine
                     + "/api/GetData?uri=uriAdress " + Environment.NewLine
                     + "e.g. " + Environment.NewLine
                     + "/api/GetData?uri=http://eal.dk" + Environment.NewLine
                     + "/api/GetData?uri=https://www.dr.dk/Nyheder/Service/feeds/regionale/fyn/#" + Environment.NewLine
                     + "/api/GetData?uri=http://webservicedemo.datamatiker-skolen.dk/PostWcfService.svc/RESTjson/GetAllePostdistrikterFraTil?fraPostnr=5000%26tilPostnr=5999" + Environment.NewLine
                     + "attention to parameter in the uri & must be replaced with %26 as it else will be extra for GetData" + Environment.NewLine;
            response.Content = new StringContent(responseText);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;

        }
    }
}
