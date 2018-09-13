using System;
using System.Collections.Generic;

namespace Toxicity.Controllers
{
    /// <summary>
    /// The class for communicating with the database
    /// </summary>
    internal class ChatBotDBhandler
    {
        
        public ChatBotDBhandler()
        {
            /* 
             * Set up connection to the database
            */


        }

        /// <summary>
        /// Get all the channels information currently in the database 
        /// </summary>
        /// <returns>A list of theese channels</returns>
        internal List<Channel> getAllChannels()
        {
            return new List<Channel> { };
        }

        /// <summary>
        /// Get a specific channel from the db
        /// </summary>
        /// <param name="id">Unique id for each channel in the database</param>
        /// <returns>The specific channel</returns>
        internal Channel getChannelinfo(int id)
        {
            return new Channel {ChannelName="TestChannel"};
        }

        internal void getChannelinfo(string channelName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if the channel exists in the database and have an active value
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        internal bool channelExistInDB(string channelName)
        {
            return false;
        }

        /// <summary>
        /// Save the channel to the database
        /// </summary>
        internal void SaveChannel(Channel newChannel)
        {
            if (validChannel(newChannel))
            {
                // Save to db, 
            }
        }

        /// <summary>
        /// Check if the Channel is legit
        /// </summary>
        /// <param name="newChannel"></param>
        /// <returns></returns>
        private bool validChannel(Channel newChannel)
        {
            return true;
        }
    }
}