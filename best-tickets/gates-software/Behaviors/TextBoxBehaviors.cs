using System.Windows;
using System.Windows.Controls;

namespace GatesSoftware.Behaviors
{
    public static class TextBoxBehaviors
    {
        public static readonly DependencyProperty MaintainCursorPositionProperty =
            DependencyProperty.RegisterAttached(
                "MaintainCursorPosition",
                typeof(bool),
                typeof(TextBoxBehaviors),
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
            if (selectionStart < textBox.Text.Length && 
                (selectionStart == 3 || selectionStart == 7) && 
                textBox.Text[selectionStart] == '-')
            {
                textBox.SelectionStart = selectionStart + 1;
            }
        }
    }
}
