using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Model;

namespace day02_ex00
{
    public class Archive<N> where N : ISearchable
    {
        [JsonPropertyName("results")] 
        public List<N> Items { get; set; }

        public static IEnumerable<N> DeserializeFile(string jsonFile)
        {
            string jsonTxt = File.ReadAllText(jsonFile);
            var item = JsonSerializer.Deserialize<Archive<N>>(jsonTxt);
            return item?.Items;
        }
    }

    class Program
    {
        static void PrintResult(IEnumerable<ISearchable> src, Media media)
        {
            IEnumerable<ISearchable> targetMedia = src.Where(t => t.MediaType == media);
            if (!targetMedia.Any())
                return;
            Console.WriteLine($"\n{media} search result [{targetMedia.Count()}]");
            Console.WriteLine(string.Join('\n', targetMedia.Select(t => t.ToString())));
        }

        static void Main(string[] args)
        {
            string search;
            
            Console.WriteLine("Input search text:");
            search = Console.ReadLine();
            
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            List<ISearchable> archive = new List<ISearchable>();

            try 
            { 
                archive.AddRange(Archive<Book>.DeserializeFile("book_reviews.json"));
                archive.AddRange(Archive<Movie>.DeserializeFile("movie_reviews.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            var serchItems = archive.Where(t => t.Title.Contains(search, comp));

            if (!serchItems.Any())
            { 
                Console.WriteLine($"There are no \"{search}\" in media today.");
                return;
            }
            Console.WriteLine($"\nItems found: {serchItems.Count()}");
            PrintResult(serchItems, Media.Book);
            PrintResult(serchItems, Media.Movie);
        }
    }
}