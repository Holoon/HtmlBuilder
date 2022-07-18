using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace HtmlBuilder;

public partial class Html
{
    internal Html(TagBuilder builder, bool selfClosed = false)
    {
        SelfClosed = selfClosed;
        TagBuilder = builder;
        IsTag = true;
    }
    internal Html(HtmlContentBuilder builder)
    {
        ContentBuilder = builder;
        IsTag = false;
    }
    internal bool IsTag { get; }
    internal bool SelfClosed { get; }
    internal TagBuilder TagBuilder { get; }
    internal HtmlContentBuilder ContentBuilder { get; }
    public IEnumerable<Html> Children { get; } = new List<Html>();
    public void AddChild(Html child) => ((List<Html>)Children).Add(child);
    public void SetText(string text)
    {
        if (IsTag && TagBuilder != null)
            _ = TagBuilder.InnerHtml.AppendLine(text);
        else if (!IsTag && ContentBuilder != null)
            _ = ContentBuilder.AppendLine(text);
    }

    public override string ToString()
    {
        using var writer = new StringWriter();
        RenderHtml().WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        writer.Flush();
        return writer.ToString();
    }
    public virtual HtmlContentBuilder RenderHtml()
    {
        var builder = new HtmlContentBuilder();
        foreach (var htmlContent in RenderRecursively(this))
            _ = builder.AppendHtml(htmlContent);
        return builder;
    }
    private static IEnumerable<IHtmlContent> RenderRecursively(Html html)
    {
        if (html == null)
            yield break;

        if (html.IsTag && html.TagBuilder != null)
        {
            if (html.SelfClosed && (html.Children == null || !html.Children.Any()))
            {
                yield return html.TagBuilder.RenderSelfClosingTag();
                yield break;
            }

            yield return html.TagBuilder.RenderStartTag();
            yield return html.TagBuilder.RenderBody();
        }
        else if (!html.IsTag && html.ContentBuilder != null)
        {
            yield return html.ContentBuilder;
        }

        foreach (var child in html.Children ?? Array.Empty<Html>())
        {
            foreach (var htmlContent in RenderRecursively(child))
                yield return htmlContent;
        }

        if (html.IsTag && html.TagBuilder != null)
            yield return html.TagBuilder.RenderEndTag();
    }
}
