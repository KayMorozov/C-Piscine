using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Program2
{
    class Program
    {
        static int LevenshteinDistance(string str1, string str2)
        {
            int i;
            int j;
            
            if (str1 == null || str2 == null)
                return (-1);
            int difference;
            int [,] table = new int[str1.Length + 1, str2.Length + 1];
            i = 0;
            j = 0;
            while (i <= str1.Length)
            {
                table[i, 0] = i;
                i++;
            }
            while (j <= str2.Length)
            {
                table[0, j] = j;
                j++;
            }
            i = 1;
            while (i <= str1.Length)
            {
                j = 1;
                while (j <= str2.Length)
                {
                    if (str1[i - 1] == str2[j - 1])
                        difference = 0;
                    else
                        difference = 1;
                    table[i, j] = Math.Min(Math.Min(table[i - 1, j] + 1, table[i, j - 1] + 1), table[i - 1, j - 1] + difference);
                    j++;
                }
                i++;
            }
            return (table[str1.Length, str2.Length]);
        }
        
        static void Main(string[] args)
        {
            string thisName;
            string path;
            string[] names;
            string answer;
            string pattern;
            int[] priority;
            int i;
            int j;
            
            path = @"us.txt";
            Console.WriteLine("Enter name:");
            thisName = Console.ReadLine();
            pattern = @"(^[A-Z -]+$)";
            if (!Regex.IsMatch(thisName, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Your name was not found.");
                return;
            }
            names = File.ReadAllLines(path);
            priority = new int[names.Length];
            i = 0;
            foreach (var name in names)
            {
                priority[i] = LevenshteinDistance(thisName, name);
                if (priority[i] == 0)
                {
                    Console.WriteLine($"Hello, {name}!");
                    return;
                }
                i++;
            }
            j = 1;
            while (j < 3)
            {
                i = 0;
                foreach (var name in names)
                {
                    if (priority[i] == j)
                    {
                        Console.WriteLine($"Did you mean \"{name}\"? Y/N");
                        answer = Console.ReadLine();
                        while (answer != "Y" && answer != "N")
                        {
                            Console.WriteLine("Y/N");
                            answer = Console.ReadLine();
                        }
                        if (answer == "Y")
                        {
                            Console.WriteLine($"Hello, {name}!");
                            return;
                        }
                    }
                    i++ ;
                }
                j++;
            }
            Console.WriteLine("Your name was not found.");
        }
    }
}
