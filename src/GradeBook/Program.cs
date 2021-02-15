using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = CreateBook();
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"");
            Console.WriteLine($"For the book named {book.Name}:");
            Console.WriteLine($"The lowest grade is {stats.Low:N1}");
            Console.WriteLine($"The highest grade is {stats.High:N1}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static IBook CreateBook()
        {
            Console.WriteLine("Please enter the student's name:");
            var name = Console.ReadLine();
            Console.WriteLine("");

            IBook book;

            while (true)
            {
                Console.WriteLine("Where do you want to store the grade book?");
                Console.WriteLine("  1. In memory");
                Console.WriteLine("  2. In a .txt file");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        book = new InMemoryBook($"{name}'s Grade Book");
                        return book;

                    case "2":
                        book = new DiskBook($"{name}'s Grade Book");
                        return book;

                    default:
                        Console.WriteLine("Invalid option.");
                        Console.WriteLine("");
                        break;
                }
            }



        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Enter a new grade or 'q' to quit:");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                System.Console.WriteLine("");
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added.");
        }
    }
}
