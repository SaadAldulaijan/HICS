using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extension
{
    class Program
    {
        // TODO: connect with CUCM to send pager.
        // TODO: Sound should be played 

        /* This will take the parameter from Activation Web site and pass it in args.
         * This App will only play a sound. 
         * This app should be on the server
         */



        static void Main(string[] args)
        {
            // Initialize a new instance of the SpeechSynthesizer.  
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the audio output.   
            synth.SetOutputToDefaultAudioDevice();

            string text = string.Join(" ", args);

            synth.Rate = -2;
            // Speak a string.  
            synth.SpeakAsync($" {text} is activated");
            Thread.Sleep(5000);
            synth.SpeakAsync($" {text} is activated");
            //synth.Speak("Code Blue in Ward 1 is activated ");
            
            //var ip = GetIP();
            //Console.WriteLine(ip);
            //Console.WriteLine(args);
            //Console.ReadKey();
        }

        //public static string GetIP()
        //{
        //    string ip = "";
        //    IPAddress [] ips = Dns.GetHostAddresses(Dns.GetHostName());
        //    foreach (var item in ips)
        //    {
        //        ip = string.Join(",", item);
        //    }
        //    return ip;
        //}
    }
}