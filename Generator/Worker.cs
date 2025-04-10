using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;

namespace TPRandomizer
{
    public class Worker
    {
        private IServiceProvider provider;

        // This constructor is called automatically by dependency injection
        // stuff when we create the instance in Program.cs
        public Worker(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Run(string[] args)
        {
            Global.Init(provider);

            // Set default to "en" so it is consistent regardless of the
            // system's language.
            Res.UpdateCultureInfo("en");

            string str;
            byte[] bytes;

            string command = args[0];

            switch (command)
            {
                case "generate_final_output2":
                    // seedId, fileCreationSettingsString, itemString, SettingsString
                    Randomizer.GenerateFinalOutput2(args[1], args[2], args[3], args[4]);
                    break;
                case "print_check_ids":
                    Console.WriteLine(
                        JsonConvert.SerializeObject(CheckIdClass.GetNameToIdNumDictionary())
                    );
                    break;
                // "dangerously_print_full_race_spoiler" should only ever be
                // called by a human manually from the command line. The website
                // must never call this code.
                default:
                    throw new Exception("Unrecognized command.");
            }
        }
    }
}
