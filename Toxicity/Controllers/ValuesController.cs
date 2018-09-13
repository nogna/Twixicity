using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TwitchToxicity;

namespace Toxicity.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
   
        public string Get([FromUri] string query)
        {
            Console.WriteLine("Call was made with this query: "+ query);
            TwitchChatBot bot = query != null ? new TwitchChatBot(query) : new TwitchChatBot();
            bot.Connect();
            Console.WriteLine("bot connected");
            var average = bot.getToxicity();
            bot.Disconnect();
            return  $"{average.ToString()} {bot.CHANNELNAME}";
            
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
