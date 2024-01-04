using System;
using System.Collections.Generic;

namespace Alfréd_a_jegyző
{
    class Program
    {
        static void Option1()
        {
            var timetable = new Dictionary<string, List<string>>
            {
                { "Hetfo", new List<string> { "", "", "Matek", "Matek", "Irodalom", "Töri", "Osztályfőnöki", "Fizika", "Angol", "" } },
                { "Kedd", new List<string> { "Angol", "Programozás", "Programozás", "Programozás", "Adatbázis", "Adatbázis", "Webprogram", "Webprogram", "Webprogram" } },
                { "Szerda", new List<string> { "", "Történelem", "Irodalom", "Irodalom", "Matek", "Tesi", "", "", "" } },
                { "Csutortok", new List<string> { "", "", "Szakmai ang", "Szakmai ang", "IKT project", "IKT project", "", "", "Tesi" } },
                { "Pentek", new List<string> { "", "", "Matek", "Nyelvtan", "Fizika", "Töri", "Angol", "Tesi", "Tesi" } }
            };

            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}", "Period", "Hetfo", "Kedd", "Szerda", "Csutortok", "Pentek");
            Console.WriteLine(new string('-', 80));

            for (int i = 0; i < 9; i++)
            {
                Console.Write("{0,-10}", i);
                foreach (var day in timetable)
                {
                    var subject = day.Value[i];
                    if (!string.IsNullOrEmpty(subject))
                    {
                        Console.Write("{0,-15}", subject);
                    }
                    else
                    {
                        Console.Write("{0,-15}", " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void Option2()
        {
            Console.WriteLine("You selected option 2.");
        }

        static void Option3()
        {
            Console.WriteLine("You selected option 3.");
        }

        static void Quit()
        {
            Console.WriteLine("Quitting...");
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            var menuOptions = new Dictionary<string, Action>
            {
                { "1", Option1 },
                { "2", Option2 },
                { "3", Option3 },
                { "q", Quit }
            };

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Option 1");
                Console.WriteLine("2. Option 2");
                Console.WriteLine("3. Option 3");
                Console.WriteLine("Q. Quit");

                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                if (menuOptions.ContainsKey(choice))
                {
                    menuOptions[choice]();
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
    }
}
