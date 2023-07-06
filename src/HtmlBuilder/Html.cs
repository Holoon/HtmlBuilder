using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmlBuilder;

public partial class Html
{
    private Html(TagBuilder builder, bool selfClosed = false)
    {
        SelfClosed = selfClosed;
        TagBuilder = builder;
        IsTag = true;
    }

    private Html(HtmlContentBuilder builder)
    {
        ContentBuilder = builder;
        IsTag = false;
    }

    private bool IsTag { get; }

    private bool SelfClosed { get; }

    private TagBuilder TagBuilder { get; }

    private HtmlContentBuilder ContentBuilder { get; }

    private IEnumerable<Html> Children { get; } = new List<Html>();

    public Html AddChild(Html child)
    {
        ((List<Html>)Children).Add(child);

        return this;
    }

    public Html SetText(string text)
    {
        switch (IsTag)
        {
            case true when TagBuilder != null:
                _ = TagBuilder.InnerHtml.SetContent(text);
                break;
            case false when ContentBuilder != null:
                _ = ContentBuilder.SetContent(text);
                break;
        }

        return this;
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
        {
            _ = builder.AppendHtml(htmlContent);
        }

        return builder;
    }

    private static IEnumerable<IHtmlContent> RenderRecursively(Html html)
    {
        if (html == null)
        {
            yield break;
        }

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
            {
                yield return htmlContent;
            }
        }

        if (html.IsTag && html.TagBuilder != null)
        {
            yield return html.TagBuilder.RenderEndTag();
        }
    }
}