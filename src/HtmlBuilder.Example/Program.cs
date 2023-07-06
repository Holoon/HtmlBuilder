using System;
using System.Collections.Generic;
using HtmlBuilder;

var root = Html.Div();

var h1 = Html.Heading(1);
h1.AddChild(Html.TextBlock("Hello World!"));
root.AddChild(h1);

var paragraph = Html.Paragraph();
var ul = Html.UnorderedList();
var li = Html.ListItem();
ul.AddChild(li);
li.AddChild(Html.TextBlock("This is a first test."));
paragraph.AddChild(ul);
var ol = Html.OrderedList();
ol.AddChild(li);
paragraph.AddChild(ol);
root.AddChild(paragraph);

root.AddChild(Html.HorizontalRule());
root.AddChild(Html.LineBreak());

root.AddChild(Html.TextBlock("This is a second test.",
    new InlineTag("strong", KeyValuePair.Create("class", "second"))));

var span = Html.Span();
span.AddChild(Html.TextBlock("Text is third test."));
root.AddChild(span);

var html = root.ToString();

root = Html.Div()
    .AddChild(
        Html.Heading(1).AddChild(Html.TextBlock("Hello World!"))
    ).AddChild(
        Html.Paragraph()
            .AddChild(
                Html.UnorderedList().AddChild(Html.ListItem().AddChild(Html.TextBlock("This is a first test.")))
            ).AddChild(
                Html.OrderedList().AddChild(Html.ListItem().AddChild(Html.TextBlock("This is a first test.")))
            )
    ).AddChild(
        Html.HorizontalRule()
    ).AddChild(
        Html.LineBreak()
    ).AddChild(
        Html.TextBlock("This is a second test.", new InlineTag("strong", KeyValuePair.Create("class", "second"))
        ).AddChild(
            Html.Span()
                .AddChild(
                    Html.TextBlock("Text is third test.")
                )
        )
    );

var htmlChain = root.ToString();

Console.Write(html);
Console.Write(htmlChain);

/* rendered html for both methods:
<div>
    <h1>Hello World!</h1>
    <p>
        <ul>
            <li>This is a first test.</li>
        </ul>
        <ol>
            <li>This is a first test.</li>
        </ol>
    </p>
    <hr /><br /><strong class="second">This is a second test.</strong><span>Text is third test.</span>
</div>
*/