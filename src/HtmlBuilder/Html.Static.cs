using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace HtmlBuilder
{
    public partial class Html
    {
        public static Html Paragraph() => EmptyTag("p");
        public static Html Div() => EmptyTag("div");
        public static Html OrderedList() => EmptyTag("ol");
        public static Html BulletList() => EmptyTag("ul");
        public static Html ListItem() => EmptyTag("li");
        public static Html CodeBlock() => EmptyTag("code");
        public static Html Blockquote() => EmptyTag("blockquote");
        private static Html EmptyTag(string tagName)
        {
            var builder = new TagBuilder(tagName);
            var tag = new Html(builder);
            return tag;
        }
        public static Html HorizontalRule()
        {
            var builder = new TagBuilder("hr");
            var tag = new Html(builder, true);
            return tag;
        }
        public static Html HardBreak()
        {
            var builder = new TagBuilder("br");
            var tag = new Html(builder, true);
            return tag;
        }
        public static Html Image(string src, string alt, string title)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("title", title);

            var tag = new Html(builder);
            return tag;
        }
        public static Html Heading(int? level)
        {
            var builder = new TagBuilder($"h{level ?? 1}");
            var tag = new Html(builder);
            return tag;
        }

        public static Html TextBloc(string text, params InlineTag[] inlines)
        {
            var builder = new HtmlContentBuilder();
            var rootTag = new Html(builder);

            Html currentTag = rootTag;
            foreach (var inline in inlines ?? Array.Empty<InlineTag>())
            {
                var childTag = MapInline(inline); 
                currentTag.AddChild(childTag);
                currentTag = childTag;
            }

            currentTag.SetText(text);
            return rootTag;
        }
        private static Html MapInline(InlineTag inlineTag)
        {
            var builder = new TagBuilder(inlineTag?.Tag ?? "div");

            foreach (var attribute in inlineTag?.Attributes ?? Array.Empty<KeyValuePair<string, string>>())
                builder.MergeAttribute(attribute.Key, attribute.Value);

            var tag = new Html(builder);
            return tag;
        }
    }
}