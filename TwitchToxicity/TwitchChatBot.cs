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
        string CHANNELNAME = "dakotaz";
        List<double> TOXICITY = new List<double>();
        int LIST_LENGTH = 5;
        

        SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();
        private string channel;

        public TwitchChatBot(string channel)
        {
            CHANNELNAME = channel;
        }

        public TwitchChatBot()
        {

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
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!, connected in channel: "+ e.Channel);
            //client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib! " + "Channel name: "+ e.Channel);
        }

        private void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine("Someone wrote this message: " + e.ChatMessage.Message);
            string text = e.ChatMessage.Message;
            var results = analyzer.PolarityScores(text);

            if (text == "!toxicity" && TOXICITY.Count() > 0)
            {
                Console.WriteLine("The current level of toxicity is: "+ TOXICITY.Average());
                client.SendMessage(e.ChatMessage.Channel, "The current level of toxicity is: " + TOXICITY.Average());
                return;
            }

            if (results.Compound == 0)
            {
                return;
            }

            var listLength = TOXICITY.Count; 
            if (listLength < LIST_LENGTH)
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
            while (TOXICITY.Count() <= LIST_LENGTH)
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