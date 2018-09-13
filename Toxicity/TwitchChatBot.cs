using System;
using System.Collections.Generic;
using System.Linq;
using Toxicity;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using VaderSharp;


namespace TwitchToxicity
{
    class TwitchChatBot
    {
        TwitchClient client;
        public string CHANNELNAME;

        List<double> TOXICITY = new List<double>();
        SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();


        private readonly int defualtListLength = 5;
        private readonly string defaultChannelName = "shroud";
        


        public TwitchChatBot(string channel)
        {
            CHANNELNAME = channel;
        }

        public TwitchChatBot()
        {
            CHANNELNAME = defaultChannelName;
        }

        internal void Connect()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(CHANNELNAME, TwitchCred.AccessToken);

            Console.WriteLine("Connecting...");
            client = new TwitchClient();
            client.Initialize(credentials, CHANNELNAME);
            Console.WriteLine("Initializing...");

            client.OnMessageReceived += onMessageReceived;
            client.OnJoinedChannel += onJoinedChannel;
            client.Connect();
        }

        private void onJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Connected in channel: "+ e.Channel);
            
        }

        private void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine("Someone wrote this message: " + e.ChatMessage.Message);
            string text = e.ChatMessage.Message;

            if (text == "!toxicity" && TOXICITY.Count() > 0)
            {
                Console.WriteLine("The current level of toxicity is: "+ TOXICITY.Average());
                client.SendMessage(e.ChatMessage.Channel, "The current level of toxicity is: " + TOXICITY.Average());
                return;
            }


            var results = analyzer.PolarityScores(text);
            Console.WriteLine(results.Negative);


            if (results.Compound == 0)
            {
                Console.WriteLine("Message was not added");
                return;
            }
            
            if (TOXICITY.Count < defualtListLength)
            {
                Console.WriteLine("Message added");
                TOXICITY.Add(results.Compound);
            }
            else
            {
                Console.WriteLine("Message replaced");
                TOXICITY.Remove(0);
                TOXICITY.Add(results.Compound);
            }

            

        }

        public double getToxicity()
        {
            while (TOXICITY.Count() <= defualtListLength)
            {
                System.Threading.Thread.Sleep(50);
            }
            return TOXICITY.Average();
        }

        public int getNumberOfElements()
        {
            return TOXICITY.Count();
        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting...");
            client.Disconnect();
        }
    }
}