using System.Collections.Generic;

namespace HtmlBuilder.Example;

static class Program
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Unused parameter", Justification = "Exemple class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Useless assignation", Justification = "Exemple class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "Exemple class")]
    static void Main(string[] args)
    {
        var root = Html.Div();

        var h1 = Html.Heading(1);
        h1.AddChild(Html.TextBlock("Hello World !"));
        root.AddChild(h1);

        var paragraph = Html.Paragraph();
        var ul = Html.BulletList();
        var li = Html.ListItem();
        paragraph.AddChild(ul);
        ul.AddChild(li);
        li.AddChild(Html.TextBlock("This is a first test."));
        root.AddChild(paragraph);

        root.AddChild(Html.HorizontalRule());

        root.AddChild(Html.TextBlock("This is a second test.", new InlineTag("strong", KeyValuePair.Create("class", "second"))));

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
    }
}
