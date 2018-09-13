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
    /// <summary>
    /// This controller handles the TwitchChatbot
    /// </summary>
    /// 
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChannelController : ApiController
    {
        readonly ChatBotDBhandler chatBotDBhandler = new ChatBotDBhandler();

        /// <summary>
        /// Get a specific channel toxicity that is not in the database
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        [Route("api/Channel/{channelName}")]
        [HttpGet]
        public List<Channel> getChannelToxicity(string channelName)
        {
           

            if (chatBotDBhandler.channelExistInDB(channelName))
            {
                return chatBotDBhandler.GetChannelinfo(channelName);
            }

            return new List<Channel>{ };

        }

        // GET: api/Channel
        /// <summary>
        /// Get all the channels info we have stored in the database
        /// </summary>
        /// <returns>All the channels in the database</returns>
        [HttpGet]
        public List<Channel> Get()
        {
            List<Channel> channels = chatBotDBhandler.getAllChannels();
            return channels;
        }

        // GET: api/Channel/5
        /// <summary>
        /// Get the channel info from our database with matching id
        /// </summary>
        /// <param name="id">identifier for the database</param>
        /// <returns></returns>
        //public Channel Get(int id)
        //{
        //    return chatBotDBhandler.getChannelinfo(id);
        //}
        
        public void Post([FromBody] string channelName)
        {
            double average = -10;
            TwitchChatBot bot = new TwitchChatBot(channelName);
            bot.Connect();
            Console.WriteLine("bot connected");
            average = bot.getToxicity();
            bot.Disconnect();
            Channel NewChannel = new Channel { ChannelName = channelName, ChannelToxicity = average };
            chatBotDBhandler.SaveChannel(NewChannel);
        }

        // PUT: api/Channel/5
        public void Put(int id, [FromBody]string channelName)
        {
           
        }

        // DELETE: api/Channel/5
        public void Delete(int id)
        {
        }
    }
}
