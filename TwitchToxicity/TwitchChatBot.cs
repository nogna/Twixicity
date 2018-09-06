using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using VaderSharp;

namespace TwitchToxicity
{
    class TwitchChatBot
    {
        TwitchClient client;
        string CHANNELNAME = "Nognaz";

        SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();

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
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine("Someone wrote this message: " + e.ChatMessage.Message);
            string text = e.ChatMessage.Message;

            var results = analyzer.PolarityScores(text);

            Console.WriteLine("Positive score: " + results.Positive);
            Console.WriteLine("Negative score: " + results.Negative);
            Console.WriteLine("Neutral score: " + results.Neutral);
            Console.WriteLine("Compound score: " + results.Compound);



        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting...");
            client.Disconnect();
        }
    }
}