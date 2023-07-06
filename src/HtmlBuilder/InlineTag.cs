using System.Collections.Generic;

namespace HtmlBuilder;

public class InlineTag
{
    public InlineTag(string tag, params KeyValuePair<string, string>[] attributes)
    {
        Tag = tag;
        Attributes = attributes;
    }

    public string Tag { get; set; }

    public IEnumerable<KeyValuePair<string, string>> Attributes { get; set; }
}