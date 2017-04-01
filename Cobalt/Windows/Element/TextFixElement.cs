using System.Windows;
using System.Windows.Controls;

namespace Cobalt.Windows.Element
{
    public class TextRadioButton : RadioButton
    {
        public TextRadioButton(string content)
        {
            var text = new TextBlock();
            text.Text = content;
            Content = text;
        }
    }
    public class TextCheckBox : CheckBox
    {
        public TextCheckBox(string content)
        {
            var text = new TextBlock();
            text.Text = content;
            Content = text;
        }
    }
}
