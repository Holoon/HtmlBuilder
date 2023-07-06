# HtmlBuilder

![HTML](/HTML.png "LOGO")

Package to build an HTML DOM in C# and export it to a `string` or `HtmlContentBuilder`. 

## Installation 

```
Install-Package Holoon.HtmlBuilder
```

Nuget package: https://www.nuget.org/packages/Holoon.HtmlBuilder/

## Usage

```c#
// Method 1 (instances)
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
span.AddChild(Html.TextBlock("Text is third test.").SetText("This is fourth test."));
root.AddChild(span);
var html = root.ToString();

// Method 2 (chaining)
var rootChain = Html.Div()
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
                ).AddChild(
                    Html.TextBlock("Text is third test.").SetText("This is fourth test.")
                )
        )
    );

var htmlChain = rootChain.ToString();

Console.Write(html);
Console.Write(htmlChain);

/* rendered html for both methods (formatted manually):
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
    <hr />
    <br />
    <strong class="second">This is a second test.</strong>
    <span>Text is third test.This is fourth test.</span>
</div>
*/
```

## Contributing
If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are welcome. Please respect existing style in code.