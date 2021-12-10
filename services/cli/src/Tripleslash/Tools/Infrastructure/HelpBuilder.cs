using System.Text;

namespace Tripleslash.Tools.Infrastructure;

public class HelpBuilder
{
    private readonly string _programName;
    private readonly string _shortDescription;
    private readonly List<(string templates, string[] docs)> _options = new(16);
    private readonly List<(string key, string[] docs)> _arguments = new(16);
    private string[]? _description;
    private readonly List<(string key, string[] docs)> _preSections = new(16);
    private readonly List<(string key, string[] docs)> _postSections = new(16);

    public HelpBuilder(string programName, string shortDescription)
    {
        _programName = programName;
        _shortDescription = shortDescription;
    }

    public HelpBuilder Option(string templates, params string[] docs)
    {
        _options.Add((templates, docs));
        return this;
    }

    public HelpBuilder Arguments(string key, params string[] docs)
    {
        _arguments.Add((key, docs));
        return this;
    }

    public HelpBuilder Description(params string[] docs)
    {
        _description = docs;
        return this;
    }

    public HelpBuilder PreSection(string key, params string[] docs)
    {
        _preSections.Add((key, docs));
        return this;
    }

    public HelpBuilder PostSection(string key, params string[] docs)
    {
        _postSections.Add((key, docs));
        return this;
    }

    public IEnumerable<string> Build()
    {
        const string indent = "   ";

        var content = new List<string>(200);

        content.Add($"NAME");
        content.Add(string.Empty);
        content.Add($"{indent}{_programName} - {_shortDescription}");
        content.Add(string.Empty);
        content.Add($"SYNOPSIS");
        content.Add(string.Empty);

        var line = $"{indent}{_programName} ";

        if (_arguments.Count > 0)
        {
            foreach (var arg in _arguments)
            {
                line += $"<{arg.key}> ";
            }
        }

        if (_options.Count > 0)
        {
            line += "[OPTIONS]";
        }

        content.Add(line);
        content.Add(string.Empty);

        if (_description != null)
        {
            content.Add("DESCRIPTION");
            content.Add(string.Empty);
            content.Add(indent + string.Join(Environment.NewLine, _description));
            content.Add(string.Empty);
        }

        foreach (var (key, docs) in _preSections)
        {
            content.Add(key);
            content.Add(string.Empty);
            content.Add(indent + string.Join(Environment.NewLine, docs));
            content.Add(string.Empty);
        }

        if (_arguments.Count > 0)
        {
            content.Add("ARGUMENTS");
            content.Add(string.Empty);
            foreach (var (key, docs) in _arguments)
            {
                content.Add($"{indent}{key}");
                content.Add(string.Empty);
                content.Add($"{indent}{indent}{string.Join(Environment.NewLine + indent + indent, docs)}");
                content.Add(string.Empty);
            }
        }
        
        if (_options.Count > 0)
        {
            content.Add("OPTIONS");
            content.Add(string.Empty);
            foreach (var (key, docs) in _options)
            {
                content.Add($"{indent}{key}");
                content.Add(string.Empty);
                content.Add($"{indent}{indent}{string.Join(Environment.NewLine + indent + indent, docs)}");
                content.Add(string.Empty);
            }
        }
        
        foreach (var (key, docs) in _postSections)
        {
            content.Add(key);
            content.Add(string.Empty);
            content.Add(indent + string.Join(Environment.NewLine, docs));
            content.Add(string.Empty);
        }

        return content;
    }
}