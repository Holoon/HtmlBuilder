using System.Collections.Generic;
using System.Linq;

namespace HtmlBuilder;

public class InlineTag
{
    public InlineTag(string tag, params KeyValuePair<string, string>[] attributes)
    {
        Tag = tag;
        if (attributes != null && attributes.Any())
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var attribute in attributes)
                dictionary.Add(attribute.Key, attribute.Value);
            Attributes = attributes;
        }
    }
    public string Tag { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Attributes { get; set; }
}
