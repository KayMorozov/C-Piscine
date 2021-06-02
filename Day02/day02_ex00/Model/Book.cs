using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class BookDetail
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("author")]
        public string Author { get; set; }
        
        [JsonPropertyName("description")]
        public string SummaryShort { get; set; }
    }

    public class Book : ISearchable
    {
        public Media MediaType => Media.Book;
        
        [JsonPropertyName("rank")]
        public int Rank { get; set; }
        
        [JsonPropertyName("list_name")]
        public string ListName { get; set; }
        
        [JsonPropertyName("amazon_product_url")]
        public string Url { get; set; }
        
        [JsonPropertyName("book_details")]
        public List<BookDetail> BookDetails { get; set; }

        public string Title => BookDetails[0].Title;
        
        public string Author => BookDetails[0].Author;
        
        public string SummaryShort => BookDetails[0].SummaryShort;

        public override string ToString()
        {
            return $"- {Title} by {Author} [{Rank} on NYT’s {ListName}]\n{SummaryShort}\n{Url}";
        }
    }
}