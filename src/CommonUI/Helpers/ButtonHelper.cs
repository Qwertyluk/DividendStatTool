using System;
using System.Windows;
using System.Windows.Controls;

namespace CommonUI.Helpers
{
    public class ButtonHelper
    {
        public static bool? GetDialogResult(DependencyObject d)
        {
            return (bool?)d.GetValue(DialogResultProperty);
        }

        public static void SetDialogResult(DependencyObject d, bool? value)
        {
            d.SetValue(DialogResultProperty, value);
        }

        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(ButtonHelper),
                new UIPropertyMetadata(DialogResultPropertyChanged));

        private static void DialogResultPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Button button)
            {
                throw new ArgumentException($"{nameof(DialogResultProperty)} can be used only with {nameof(Button)} control");
            }

            button.Click += (s, e) =>
            {
                Window.GetWindow(button).DialogResult = GetDialogResult(button);
            };
        }
    }
}
