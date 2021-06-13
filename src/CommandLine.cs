// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;

namespace LogApp
{
    /// <summary>
    /// Main application class
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// Build the RootCommand for parsing
        /// </summary>
        /// <returns>RootCommand</returns>
        public static RootCommand BuildRootCommand()
        {
            RootCommand root = new RootCommand
            {
                Name = "LogApp",
                Description = "Test FluentBit logging",
                TreatUnmatchedTokensAsErrors = true,
            };

            root.AddOption(new Option<int>(new string[] { "--iterations", "-i" }, () => 1, "Iterations to log"));
            root.AddOption(new Option<int>(new string[] { "--sleep", "-s" }, () => 0, "Sleep (ms) between each iteration"));
            root.AddOption(new Option<bool>(new string[] { "--loop", "-l" }, () => false, "Run infinite loop (-s must be > 0)"));
            root.AddOption(new Option<bool>(new string[] { "--dry-run", "-d" }, "Validates parameters"));

            root.AddValidator(ValidateArgs);

            return root;
        }

        // validate command line args
        private static string ValidateArgs(CommandResult result)
        {
            string msg = string.Empty;

            try
            {
                int iterations = !(result.Children.FirstOrDefault(c => c.Symbol.Name == "iterations") is OptionResult iterationsRes) ? 1 : iterationsRes.GetValueOrDefault<int>();
                int sleep = !(result.Children.FirstOrDefault(c => c.Symbol.Name == "sleep") is OptionResult sleepRes) ? 0 : sleepRes.GetValueOrDefault<int>();
                bool loop = !(result.Children.FirstOrDefault(c => c.Symbol.Name == "loop") is OptionResult loopRes) ? false : loopRes.GetValueOrDefault<bool>();

                // validate sleep
                if (loop && sleep < 1)
                {
                    msg += "--sleep must be > 0\n";
                }

                // validate iterations
                else if (!loop && iterations < 1)
                {
                    msg += "--iterations must be > 0\n";
                }

                // validate sleep
                if (!loop && sleep < 0)
                {
                    msg += "--sleep must be >= 0\n";
                }
            }
            catch
            {
                // system.commandline will catch and display parse exceptions
            }

            // return error message(s) or string.empty
            return msg;
        }

        // handle --dry-run
        private static int DoDryRun(int iterations, int sleep)
        {
            DisplayAsciiArt();

            // display the config
            Console.WriteLine("dry run");
            Console.WriteLine($"   Iterations  {iterations}");
            Console.WriteLine($"   Sleep       {sleep}");

            return 0;
        }

        // Display the ASCII art file if it exists
        private static void DisplayAsciiArt()
        {
            const string file = "ascii-art.txt";

            if (File.Exists(file))
            {
                Console.WriteLine(File.ReadAllText(file));
            }
        }
    }
}
