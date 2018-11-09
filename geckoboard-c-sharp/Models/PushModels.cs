using Newtonsoft.Json;

namespace geckoboardcsharp.Models
{
    /// <summary>
    /// The result
    /// </summary>
    public class PushResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}