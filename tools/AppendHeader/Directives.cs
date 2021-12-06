using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppendHeaders
{
    public class Directives
    {
        [JsonPropertyName("include")]
        public List<string>? IncludePatterns { get; set; } = new()
        {
            "**/*.cs"
        };

        [JsonPropertyName("exclude")]
        public List<string>? ExcludePatterns { get; set; } = new()
        {
            "bin/**/*",
            "obj/**/*"
        };
        
        public List<string>? Header { get; set; }
    }
}