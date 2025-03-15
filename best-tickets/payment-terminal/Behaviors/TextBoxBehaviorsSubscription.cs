using System.Windows;
using System.Windows.Controls;

namespace paymentterminal.Behaviors
{
    public static class TextBoxBehaviorsSubscription
    {
        public static readonly DependencyProperty MaintainCursorPositionProperty =
            DependencyProperty.RegisterAttached(
                "MaintainCursorPosition",
                typeof(bool),
                typeof(TextBoxBehaviorsSubscription),
                new PropertyMetadata(false, OnMaintainCursorPositionChanged));

        public static bool GetMaintainCursorPosition(TextBox textBox)
        {
            return (bool)textBox.GetValue(MaintainCursorPositionProperty);
        }

        public static void SetMaintainCursorPosition(TextBox textBox, bool value)
        {
            textBox.SetValue(MaintainCursorPositionProperty, value);
        }

        private static void OnMaintainCursorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.TextChanged += TextBox_TextChanged;
                }
                else
                {
                    textBox.TextChanged -= TextBox_TextChanged;
                }
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            int selectionStart = textBox.SelectionStart;

            // If the selection start is at the position of a dash, move it one position forward
            // For subscription format xxxx-xxxx, the dash is at position 4
            if (selectionStart < textBox.Text.Length && 
                selectionStart == 4 && 
                textBox.Text[selectionStart] == '-')
            {
                textBox.SelectionStart = selectionStart + 1;
            }
        }
    }
}
