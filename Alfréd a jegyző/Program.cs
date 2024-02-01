using System;
using System.Collections.Generic;

namespace Alfréd_a_jegyző
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu(new List<MenuOption>
            {
                new MenuOption { Description = "Órarend", Function = menu => { Orarend(menu); } },
                new MenuOption { Description = "Jegyek kezelése", Function = menu => { Jegyek(menu); } },
                new MenuOption { Description = "Hiányzó diákok jelentése", Function = menu => { ReportMissingStudents(menu); } },
            });

            menu.Display();
        }

        class MenuOption
        {
            public string Description { get; set; }
            public Action<Menu> Function { get; set; }
        }

        class Menu
        {
            private List<MenuOption> options;
            private bool quit = false;

            public Menu(List<MenuOption> options)
            {
                this.options = options;
            }

            public void Display()
            {
                while (!quit)
                {
                    for (int i = 0; i < options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {options[i].Description}");
                    }

                    Console.WriteLine("q. Kilépés");
                    Console.Write("Válassz egy opciót:");
                    var input = Console.ReadLine();

                    if (input.ToLower() == "q")
                    {
                        Quit();
                        break;
                    }

                    if (int.TryParse(input, out int option) && option >= 1 && option <= options.Count)
                    {
                        Console.Clear();
                        options[option - 1].Function(this);
                        //Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Alfréd nem érti a parancsot.");
                    }
                }
            }

            public void Quit()
            {
                quit = true;
            }
        }

        static void Orarend(Menu menu)
        {
            Console.Clear();
            var timetable = new Dictionary<string, List<string>>
            {
                { "Hetfo", new List<string> { "", "Matek", "Matek", "Irodalom", "Töri", "Osztályfőnöki", "Fizika", "Angol", "" } },
                { "Kedd", new List<string> { "", "Programozás", "Programozás", "Programozás", "Adatbázis", "Adatbázis", "Webprogram", "Webprogram", "Webprogram" } },
                { "Szerda", new List<string> { "Angol", "Történelem", "Irodalom", "Irodalom", "Matek", "Tesi", "", "", "" } },
                { "Csutortok", new List<string> { "IKT project", "IKT project", "Szakmai ang", "Szakmai ang", "IKT project", "IKT project", "", "", "" } },
                { "Pentek", new List<string> { "", "", "Matek", "Nyelvtan", "Fizika", "Töri", "Angol", "Tesi", "Tesi" } }
            };

            Console.WriteLine($"{"Nap",-10} {"Hétfő",-15} {"Kedd",-15} {"Szerda",-15} {"Csütörtök",-15} {"Pentek",-15}");
            Console.WriteLine(new string('-', 80));

            for (int i = 0; i < 9; i++)
            {
                Console.Write($"{i,-10}");
                foreach (var day in timetable)
                {
                    var subject = day.Value[i];
                    if (!string.IsNullOrEmpty(subject))
                    {
                        Console.Write($"{subject,-15}");
                    }
                    else
                    {
                        Console.Write($"{" ",-15}");
                    }
                }
                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }
        
        static Dictionary<int, Tuple<string, List<int>, bool>> students = new Dictionary<int, Tuple<string, List<int>, bool>>
        {
            { 1, new Tuple<string, List<int>, bool>(HungarianNameGenerator.Generate(), new List<int> { 5, 4, 3, 2, 1 }, false) },
            { 2, new Tuple<string, List<int>, bool>(HungarianNameGenerator.Generate(), new List<int> { 1, 2, 3, 4, 5 }, false) },
            { 3, new Tuple<string, List<int>, bool>(HungarianNameGenerator.Generate(), new List<int> { 2, 3, 4, 5, 1 }, false) },
            { 4, new Tuple<string, List<int>, bool>(HungarianNameGenerator.Generate(), new List<int> { 3, 4, 5, 1, 2 }, false) },
            { 5, new Tuple<string, List<int>, bool>(HungarianNameGenerator.Generate(), new List<int> { 4, 5, 1, 2, 3 }, false) }
        };


        static void Jegyek(Menu menu)
        {
            Console.Clear();
            Console.WriteLine($"{"Azonosító",-10} {"Név",-20}");
            Console.WriteLine(new string('-', 80));

            foreach (var student in students)
            {
                Console.Write($"{student.Key,-10}");
                Console.Write($"{student.Value.Item1,-20}");
                foreach (var grade in student.Value.Item2)
                {
                    Console.Write($"{grade,-5}");
                }
                Console.WriteLine();
            }

            //Jegy hozzáadás
            Console.WriteLine("1. Jegyek kezelése");
            Console.Write("Válassz egy opciót vagy lépj ki bármilyen más billentyűvel: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddGrade(menu);
                    break;
                default:
                    Console.Clear();
                    return;
            }
        }

        static void AddGrade(Menu menu)
        {
            Console.Write("Diák azonosító:");
            var id = int.Parse(Console.ReadLine());

            Console.Write("Jegy:");
            var grade = int.Parse(Console.ReadLine());

            if (students.ContainsKey(id))
            {
                students[id].Item2.Add(grade);
            }
            else
            {
                Console.WriteLine("Alfréd nem talál ilyen diákot.");
            }
            Console.Clear();
            Jegyek(menu);
        }

        static void ReportMissingStudents(Menu menu)
        {
            Console.Clear();
            Console.WriteLine("Hiányzó diákok jelentése");
            Console.WriteLine(new string('-', 80));

            foreach (var student in students)
            {
                Console.Write($"{student.Key,-10}");
                Console.Write($"{student.Value.Item1,-30}");
                Console.Write(student.Value.Item3 ? "[X]" : "[ ]"); // Show check mark for missing students
                Console.WriteLine();
            }

            Console.WriteLine("\nNyomj Entert a kijelölt diák hiányzásának jelentésére.");
            Console.WriteLine("Nyomj Esc-et a visszalépéshez.");

            // Handling user input
            var key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    Console.Write("\nDiák azonosítója: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        if (students.ContainsKey(id))
                        {
                            students[id] = new Tuple<string, List<int>, bool>(students[id].Item1, students[id].Item2, true); // Mark student as missing
                            Console.WriteLine("Hiányzás sikeresen jelentve.");
                        }
                        else
                        {
                            Console.WriteLine("Nincs ilyen azonosítójú diák.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Érvénytelen azonosító formátum.");
                    }
                    Console.ReadKey();
                    ReportMissingStudents(menu); // Restart the function after reporting
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    menu.Display(); // Return to main menu
                    break;
                default:
                    Console.Clear();
                    ReportMissingStudents(menu);
                    break;
            }
        }


        public static class HungarianNameGenerator
        {
            private static List<string> firstNames = new List<string> { "Béla", "József", "István", "Zoltán", "Péter", "Gábor", "Attila", "László", "Mihály", "György" };
            private static List<string> lastNames = new List<string> { "Kovács", "Szabó", "Horváth", "Tóth", "Varga", "Nagy", "Farkas", "Papp", "Lakatos", "Molnár" };

            public static string Generate()
            {
                Random rand = new Random();
                string firstName = firstNames[rand.Next(firstNames.Count)];
                string lastName = lastNames[rand.Next(lastNames.Count)];

                return $"{firstName} {lastName}";
            }
        }
    }
}
