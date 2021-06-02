using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model
{
    public class MovieLink
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
    
    public class Movie : ISearchable
    {
        public Media MediaType => Media.Movie;
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("mpaa_rating")]
        public string Rating { get; set; }
        
        [JsonPropertyName("critics_pick")]
        public int IsCriticsPick { get; set; }
        
        [JsonPropertyName("summary_short")]
        public string SummaryShort { get; set; }
        
        [JsonPropertyName("link")]
        public MovieLink Link { get; set; }

        public string Url => Link.Url;

        public override string ToString()
        {
            return $"- {Title}{(IsCriticsPick == 1 ? "[NYT critic’s pick]" : "")}\n{SummaryShort}\n{Url}";
        }
    }
}