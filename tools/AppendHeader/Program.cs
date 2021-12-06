using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace AppendHeaders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var path = args.FirstOrDefault() ?? Directory.GetCurrentDirectory();
                
                var directives = JsonSerializer.Deserialize<Directives>(
                    File.ReadAllText(Path.Combine(path, "header.json")),
                    new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
                
                var matcher = new Matcher();
                
                foreach (var pattern in directives!.IncludePatterns ?? Enumerable.Empty<string>())
                {
                    matcher.AddInclude(pattern);
                }

                foreach (var pattern in directives!.ExcludePatterns ?? Enumerable.Empty<string>())
                {
                    matcher.AddExclude(pattern);
                }

                var results = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(path)));
                var configuredHeader = directives.Header ?? throw new ApplicationException("Header content not found in directives file");

                const bool headerMode = true;
                const bool bodyMode = false;

                var scanned = 0;
                var modified = 0;

                foreach (var file in results.Files.Select(r => Path.Combine(path, r.Path)))
                {
                    scanned++;
                    
                    var currentHeader = ReadFile(file, headerMode);
                    
                    if (HeadersEqual(currentHeader, configuredHeader))
                        continue;

                    var nonHeaderContent = ReadFile(file, bodyMode);
                    
                    using var writer = new StreamWriter(new FileStream(file, FileMode.Create));

                    WriteStrings(writer, configuredHeader);
                    WriteStrings(writer, nonHeaderContent);
                    writer.Flush();
                    
                    Console.WriteLine($"Wrote {Path.Combine(path, file)}");

                    modified++;
                }
                
                Console.WriteLine($"Scanned {scanned}, modified {modified} file(s)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void WriteStrings(StreamWriter writer, List<string> content)
        {
            foreach (var line in content)
            {
                writer.WriteLine(line);   
            }
        }

        private static bool HeadersEqual(IReadOnlyList<string> a, IReadOnlyList<string> b)
        {
            if (a.Count != b.Count)
                return false;

            for (var c = 0; c < a.Count; c++)
            {
                if (string.Compare(a[c], b[c], StringComparison.Ordinal) != 0)
                    return false;
            }

            return true;
        }

        private static List<string> ReadFile(string file, bool header)
        {
            var results = new List<string>(16);
            var pastHeader = false;

            using var reader = new StreamReader(new FileStream(file, FileMode.Open));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line == null)
                {
                    return results;
                }

                var trimmed = line.Trim();

                var isHeader = !pastHeader
                               && (trimmed.StartsWith("/*")
                                   || trimmed.StartsWith("//")
                                   || trimmed.StartsWith("*")
                                   || trimmed.StartsWith("*/")
                                   || trimmed.Length == 0);

                switch (isHeader)
                {
                    case true when header:
                        results.Add(line);
                        break;
                    
                    case true when !header:
                        break;
                    
                    case false when header:
                        return results;
                    
                    case false when !header:
                        pastHeader = true;
                        results.Add(line);
                        break;
                }
            }

            return results;
        }
    }
}


