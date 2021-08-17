# HtmlBuilder

![HTML](/HTML.png "LOGO")

Tool to build an HTML DOM in C# and export it to a string or to an IHtmlContent Enumerable. 

## Installation 

```
Install-Package Holoon.HtmlBuilder
```

Nuget package: https://www.nuget.org/packages/Holoon.HtmlBuilder/

## Usage

```c#
var root = Html.Div();

var h1 = Html.Heading(1);
h1.AddChild(Html.TextBloc("Hello World !"));
root.AddChild(h1);

var paragraph = Html.Paragraph();
var ul = Html.BulletList();
var li = Html.ListItem();
paragraph.AddChild(ul);
ul.AddChild(li);
li.AddChild(Html.TextBloc("This is a first test."));
root.AddChild(paragraph);

root.AddChild(Html.HorizontalRule());

root.AddChild(Html.TextBloc("This is a second test.", new InlineTag("strong", KeyValuePair.Create("class", "second"))));

var html = root.ToString();
/* html:
<div>
    <h1>Hello World !</h1>
    <p>
        <ul>
            <li>This is a first test.</li>
        </ul>
    </p>
    <hr />
    <strong class="second">This is a second test.</strong>
</div>
*/
```

## Contributing
Pull requests are welcome. If you'd like to contribute, please fork the repository and use a feature branch. Please respect existing style in code.

**Important notes for pull requests** : This project use *GitFlow*, please branch from `develop`, not `master`.