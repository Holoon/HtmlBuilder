using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmlBuilder;

public partial class Html
{
    public static Html Paragraph() => EmptyTag("p");

    public static Html Div() => EmptyTag("div");

    public static Html Span() => EmptyTag("span");

    public static Html OrderedList() => EmptyTag("ol");

    public static Html UnorderedList() => EmptyTag("ul");

    public static Html ListItem() => EmptyTag("li");

    public static Html CodeBlock() => EmptyTag("code");

    public static Html Blockquote() => EmptyTag("blockquote");

    public static Html CustomTag(string tagName, params KeyValuePair<string, string>[] attributes)
    {
        var builder = new TagBuilder(tagName ?? "div");

        foreach (var attribute in attributes ?? Array.Empty<KeyValuePair<string, string>>())
        {
            builder.MergeAttribute(attribute.Key, attribute.Value);
        }

        var tag = new Html(builder);
        return tag;
    }

    public static Html HorizontalRule()
    {
        var builder = new TagBuilder("hr");
        var tag = new Html(builder, true);
        return tag;
    }

    public static Html LineBreak()
    {
        var builder = new TagBuilder("br");
        var tag = new Html(builder, true);
        return tag;
    }

    public static Html Image(string src, string alt, string title) => CustomTag(
        "img",
        KeyValuePair.Create("src", src),
        KeyValuePair.Create("alt", alt),
        KeyValuePair.Create("title", title));

    public static Html Heading(int? level) => EmptyTag($"h{level ?? 1}");

    public static Html TextBlock(string text, params InlineTag[] inlines)
    {
        var builder = new HtmlContentBuilder();
        var rootTag = new Html(builder);

        var currentTag = rootTag;
        foreach (var inline in inlines ?? Array.Empty<InlineTag>())
        {
            var childTag = CustomTag(inline?.Tag, inline?.Attributes?.ToArray());
            currentTag.AddChild(childTag);
            currentTag = childTag;
        }

        currentTag.SetText(text);
        return rootTag;
    }

    private static Html EmptyTag(string tagName) => CustomTag(tagName);
}