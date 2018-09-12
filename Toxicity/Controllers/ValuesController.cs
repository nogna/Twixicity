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
            TwitchChatBot bot = /*query != null ? new TwitchChatBot(query) :*/ new TwitchChatBot();
            bot.Connect();
            var average = bot.getToxicity();
            bot.Disconnect();
            return new string[] { average.ToString(), bot.defaultChannelName };     
        }

        // GET api/values/5
        public string Get(string query)
        {
            return $"{query}";
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
