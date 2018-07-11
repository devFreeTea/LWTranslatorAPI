using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LWTranslatorAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            string JSONresult = await DoIt();

            return JSONresult;
        }

        private static async Task<string> DoIt()
        {
            using (var stringContent = new StringContent("{\"text\": [\"Hello, world!\", \"How are you?\"], \"model_id\":\"en-es\"}", System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    var byteArray = new UTF8Encoding().GetBytes("apikey:d4Vx-98ZP1_zxJ1fY-vANj21i1WPCfD5CFBBKGPnR0e_");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    var response = await client.PostAsync("https://gateway.watsonplatform.net/language-translator/api/v3/translate?version=2018-05-01", stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
