// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogApp
{
    /// <summary>
    /// Main application class
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// Gets or sets json serialization options
        /// </summary>
        public static JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        /// <summary>
        /// Gets or sets cancellation token
        /// </summary>
        public static CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command Line Parameters</param>
        /// <returns>0 on success</returns>
        public static async Task<int> Main(string[] args)
        {
            // create arg list
            List<string> argList = args == null ? new List<string>() : new List<string>(args);

            // build the System.CommandLine.RootCommand
            RootCommand root = BuildRootCommand();
            root.Handler = CommandHandler.Create<int, int, bool, bool>(RunApp);

            // display ascii art on help
            if (argList.Contains("-h") || argList.Contains("--help"))
            {
                DisplayAsciiArt();
            }

            // add values from env vars
            argList.AddFromEnvironment("--iterations", "-i");
            argList.AddFromEnvironment("--sleep", "-s");
            argList.AddFromEnvironment("--loop", "-l");

            // add ctl-c handler
            AddControlCHandler();

            // run the app
            return await root.InvokeAsync(argList.ToArray()).ConfigureAwait(false);
        }

        /// <summary>
        /// System.CommandLine.CommandHandler implementation
        /// </summary>
        /// <param name="iterations">number of logs items to create</param>
        /// <param name="sleep">sleep (ms) between iterations</param>
        /// <param name="loop">run in endless loop</param>
        /// <param name="dryRun">dry run</param>
        /// <returns>non-zero on failure</returns>
        public static async Task<int> RunApp(int iterations, int sleep, bool loop, bool dryRun)
        {
            // don't run the test on a dry run
            if (dryRun)
            {
                return DoDryRun(iterations, sleep);
            }

            App.JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            Random rand = new Random(DateTime.UtcNow.Millisecond);

            int i = 0;

            // run the app
            try
            {
                while (loop || i < iterations)
                {
                    // sleep between iterations
                    if (i > 0 && sleep > 0)
                    {
                        await Task.Delay(sleep);
                    }

                    switch (i % 10)
                    {
                        case 1:
                            LogError(rand);
                            break;
                        case 5:
                            LogWarning(rand);
                            break;
                        default:
                            LogItem(rand);
                            break;
                    }

                    // keep i in bounds
                    i = loop && i > 999 ? 100 : i + 1;

                    // ctl-c or sigint
                    if (TokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                }

                // success
                return 0;
            }
            catch (TaskCanceledException tce)
            {
                // log exception
                if (!tce.Task.IsCompleted)
                {
                    return LogException(tce);
                }

                // task is completed
                return 0;
            }
            catch (Exception ex)
            {
                return LogException(ex);
            }
        }

        private static int LogException(Exception ex)
        {
            // generate sample error
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "date", DateTime.UtcNow },
                { "statusCode", 999 },
                { "message", ex.Message },
                { "exception", ex.ToString() },
            };

            // write the log to stderr
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(JsonSerializer.Serialize(log, JsonSerializerOptions));
            Console.ResetColor();

            return 1;
        }

        private static void LogItem(Random rand)
        {
            // generate some sample data
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "date", DateTime.UtcNow },
                { "statusCode", 200 },
                { "path", "/log/app" },
                { "duration", rand.Next(20, 100) },
                { "value", GetRandomString(rand, 10) },
            };

            // write the log to stdout
            Console.WriteLine(JsonSerializer.Serialize(log, JsonSerializerOptions));
        }

        private static void LogWarning(Random rand)
        {
            // generate some sample data
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "date", DateTime.UtcNow },
                { "statusCode", 400 },
                { "path", "/log/app" },
                { "duration", rand.Next(1, 10) },
                { "message", $"Invalid paramater: {GetRandomString(rand, 6)}" },
            };

            // write the log to stdout
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(JsonSerializer.Serialize(log, JsonSerializerOptions));
            Console.ResetColor();
        }

        private static void LogError(Random rand)
        {
            // generate sample error
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "date", DateTime.UtcNow },
                { "statusCode", 500 },
                { "path", "/log/app" },
                { "duration", rand.Next(200, 1000) },
                { "message", $"Server error {rand.Next(9001, 9999)}" },
            };

            // write the log to stderr
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(JsonSerializer.Serialize(log, JsonSerializerOptions));
            Console.ResetColor();
        }

        private static string GetRandomString(Random rand, int length)
        {
            string s = string.Empty;
            int val;

            for (int i = 0; i < length; i++)
            {
                val = rand.Next(65, 65 + 52);
                val = val > 90 ? val + 6 : val;
                s += Convert.ToChar(val);
            }

            return s;
        }

        /// <summary>
        /// Add a ctl-c handler
        /// </summary>
        private static void AddControlCHandler()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                TokenSource.Cancel();
            };
        }
    }
}
