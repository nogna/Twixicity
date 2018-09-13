using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Toxicity.Models;

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
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionStringHelper.CnnVal("TwixicityDB")))
            {
                var AllChannels = connection.Query<Channel>("SELECT * FROM Channel").AsList();
                return AllChannels;
            }
            
        }


        internal List<Channel> GetChannelinfo(string channelName)
        {
            
             using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionStringHelper.CnnVal("TwixicityDB")))
             {
                 List<Channel> channel = connection.Query<Channel>($"SELECT * FROM Channel WHERE ChannelName ='{channelName}'").AsList();
                 return channel;
             }

            
        }

        /// <summary>
        /// Returns true if exists in DB
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        internal bool channelExistInDB(string channelName)
        {
            IEnumerable<Channel> channel;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionStringHelper.CnnVal("TwixicityDB")))
            {
                channel = connection.Query<Channel>($"SELECT * FROM Channel WHERE ChannelName ='{channelName}'");
            }

            if (channel.AsList().Count() != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Save the channel to the database
        /// </summary>
        internal void SaveChannel(Channel newChannel)
        {
            if (validChannel(newChannel))
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionStringHelper.CnnVal("TwixicityDB")))
                {
                    connection.Execute("INSERT INTO Channel (ChannelName, ChannelToxicity) VALUES('" + newChannel.ChannelName + "', '" + newChannel.ChannelToxicity +"')");
                    
                }
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