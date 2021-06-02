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
    public class Archive<N>
    {
        [JsonPropertyName("results")] 
        public object[] Items { get; set; }

        public List<N> GetItemList()
        {
            List<N> itemList = new List<N>();
            foreach (var item in Items)
                itemList.Add(JsonSerializer.Deserialize<N>(item.ToString()));
            return (itemList);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string needed;
            
            Console.WriteLine("Input search text:");
            needed = Console.ReadLine();
            
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            var books = JsonSerializer.Deserialize<Archive<Book>>(File.ReadAllText("book_reviews.json"));
            List<Book> bookList = new List<Book>();
            bookList = books.GetItemList();

            var movies = JsonSerializer.Deserialize<Archive<Movie>>(File.ReadAllText("movie_reviews.json"));
            List<Movie> movieList = new List<Movie>();
            movieList = movies.GetItemList();
        }
    }
}