using System;
using System.Collections.Generic;

namespace Alfréd_a_jegyző
{
    class Program
    {
        class MenuOption
        {
            public string Description { get; set; }
            public Action Function { get; set; }
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
                    Console.WriteLine("q. Quit");
                    Console.Write("Select an option: ");
                    var input = Console.ReadLine();

                    if (input.ToLower() == "q")
                    {
                        this.Quit();
                    }

                    if (int.TryParse(input, out int option) && option >= 1 && option <= options.Count)
                    {
                        options[option - 1].Function();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }

                    if (!quit)
                    {
                        Console.WriteLine("Press any key to go back to the menu or 'q' to quit.");
                        var key = Console.ReadKey();
                        if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                        {
                            this.Quit();
                        }
                        Console.Clear();
                    }
                }
            }

            public void Quit()
            {
                quit = true;
            }
        }

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

        static Dictionary<int, Tuple<string, List<int>>> students = new Dictionary<int, Tuple<string, List<int>>>
        {
            { 1, new Tuple<string, List<int>>("Student1", new List<int> { 5, 4, 3, 2, 1 }) },
            { 2, new Tuple<string, List<int>>("Student2", new List<int> { 1, 2, 3, 4, 5 }) },
            { 3, new Tuple<string, List<int>>("Student3", new List<int> { 2, 3, 4, 5, 1 }) },
            { 4, new Tuple<string, List<int>>("Student4", new List<int> { 3, 4, 5, 1, 2 }) },
            { 5, new Tuple<string, List<int>>("Student5", new List<int> { 4, 5, 1, 2, 3 }) }
        };

        static void Option2()
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10}", "ID", "Name", "Grade1", "Grade2", "Grade3", "Grade4", "Grade5");
            Console.WriteLine(new string('-', 70));

            foreach (var student in students)
            {
                Console.Write("{0,-10}", student.Key);
                Console.Write("{0,-10}", student.Value.Item1);
                foreach (var grade in student.Value.Item2)
                {
                    Console.Write("{0,-10}", grade);
                }
                Console.WriteLine();
            }
        }

        static void AddGradeOption()
        {
            Console.WriteLine("1. Add Grade to Student");
            Console.WriteLine("2. Go back to main menu");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddGrade();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        static void AddGrade()
        {
            Console.Write("Enter student ID: ");
            var id = int.Parse(Console.ReadLine());

            Console.Write("Enter grade: ");
            var grade = int.Parse(Console.ReadLine());

            if (students.ContainsKey(id))
            {
                students[id].Item2.Add(grade);
            }
            else
            {
                Console.WriteLine("Invalid student ID.");
            }
            Console.Clear();
            Option2();
            AddGradeOption();
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
            var menu = new Menu(new List<MenuOption>
            {
                new MenuOption { Description = "Timetable", Function = Option1 },
                new MenuOption { Description = "Show Student Grades", Function = () => { Option2(); AddGradeOption(); } },
                new MenuOption { Description = "Option 3", Function = Option3 }
            });

            menu.Display();
        }
    }
}
