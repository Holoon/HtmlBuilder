using System.Collections.Generic;

namespace HtmlBuilder.Example
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
