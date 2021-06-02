using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public enum Media
    {
        Book,
        Movie
    }
    
    public interface ISearchable
    {
        string Title { get; }
        Media MediaType { get; }
    }
}