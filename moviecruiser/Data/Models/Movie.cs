using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace moviecruiser.Data.Models
{
  public class Movie
    {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int id { get; set; }
    public string name { get; set; }
    public string comments { get; set; }

    [JsonProperty(PropertyName = "poster_path")]
    public string posterPath { get; set; }

    [JsonProperty(PropertyName = "release_date")]
    public string releaseDate { get; set; }

    [JsonProperty(PropertyName = "vote_average")]
    public double voteAverage { get; set; }

    [JsonProperty(PropertyName = "vote_count")]
    public int voteCount { get; set; }
  }
}
