using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace env_clean
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var search = string.Empty;
            bool clean = false, find = false, user = false, write = false;

            try
            {
                var commands = args.Select((value, index) => new { value = args[index], index });

                // command parser
                foreach (var cmd in commands)
                {
                    switch (cmd.value.ToLower())
                    {
                        case "/c":
                        case "/clean":
                            clean = true;
                            break;

                        case "/w":
                        case "/write":
                            write = true;
                            break;

                        case "/u":
                        case "/user":
                            user = true;
                            break;

                        case "/f":
                        case "/find":
                            search = args[cmd.index + 1];
                            find = true;
                            break;
                    }
                }

                var list = GetEnv(clean, user, find, search);

                list.ForEach(Console.WriteLine);

                Console.WriteLine("There are {0} items in the list", list.Count);

                if (write)
                    WriteEnv(list, user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void WriteEnv(IEnumerable<string> list, bool user)
        {
            Console.WriteLine("Are you sure you want to write to the $env:path? (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
                Environment.SetEnvironmentVariable("Path", string.Join(";", list), user ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Machine);
            else
                Console.WriteLine("Okay, nothing was written");
        }

        private static List<string> GetEnv(bool clean, bool user, bool find, string search)
        {
            var env = (Environment.GetEnvironmentVariable("Path", user ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Machine) ?? string.Empty).Split(';');

            var list = clean ? env.Distinct().Where(Directory.Exists) : env;

            return find
                ? list.Where(item => item.Contains(search)).OrderBy(item => item).ToList()
                : list.OrderBy(item => item).ToList();
        }
    }
}