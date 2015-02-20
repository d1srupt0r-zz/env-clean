using System;
using System.Collections.Generic;
using System.Linq;

namespace env_clean
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var commands = args.Select((value, index) => new { value = args[index], index });

            try
            {
                // command parser
                foreach (var cmd in commands)
                {
                    switch (cmd.value.ToLower())
                    {
                    }
                }

                foreach (var entry in GetEnv())
                {
                    Console.WriteLine(entry);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IEnumerable<string> GetEnv()
        {
            var env = Environment.GetEnvironmentVariable("Path");

            return env != null ? env.Split(';') : new[] { "" };
        }
    }
}