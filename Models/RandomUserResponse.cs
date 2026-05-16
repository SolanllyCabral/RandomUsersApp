using System.Text.Json.Serialization;

namespace RandomUsersApp.Models
{
    public class RandomUserResponse
    {
        [JsonPropertyName("results")]
        public List<RandomUserResult> Results { get; set; } = new();
    }

    public class RandomUserResult
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public UserName Name { get; set; } = new();

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public UserLocation Location { get; set; } = new();
    }

    public class UserName
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("first")]
        public string First { get; set; } = string.Empty;

        [JsonPropertyName("last")]
        public string Last { get; set; } = string.Empty;
    }

    public class UserLocation
    {
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
    }
}