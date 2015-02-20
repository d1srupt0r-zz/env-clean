using System;
using System.Collections.Generic;

namespace env_clean
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = GetEnv();

            foreach (var p in path)
            {
                Console.WriteLine(p);
            }
        }

        private static IEnumerable<string> GetEnv()
        {
            var env = Environment.GetEnvironmentVariable("Path");

            return env != null ? env.Split(';') : new[] { "" };
        }
    }
}