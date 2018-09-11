using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using VaderSharp;


namespace TwitchToxicity
{
    class TwitchChatBot
    {
        TwitchClient client;
        string CHANNELNAME;

        List<double> TOXICITY = new List<double>();
        SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();


        private readonly int defualtListLength = 5;
        private readonly string defaultChannelName = "dakotaz";
        


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
            Console.WriteLine("Successfully connected");
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



            if (results.Compound == 0)
            {
                return;
            }
            
            if (TOXICITY.Count < defualtListLength)
            {
                TOXICITY.Add(results.Compound);
            }
            else
            {
                TOXICITY.Remove(0);
                TOXICITY.Add(results.Compound);
            }

            

        }

        public double getToxicity()
        {
            while (TOXICITY.Count() <= defualtListLength)
            {
                System.Threading.Thread.Sleep(500);
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