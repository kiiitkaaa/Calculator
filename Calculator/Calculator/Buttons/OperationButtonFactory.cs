using Calculator.Config;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator.Buttons
{
    class OperationButtonFactory(ElementStyle style) : IButtonFactory
    {
        private readonly ElementStyle style = style;

        public Button CreateButton(string content, RoutedEventHandler clickHandler)
        {
            var button = new Button
            {
                Content = content,
                Background = new BrushConverter().ConvertFromString(style.Background) as Brush,
                Foreground = new BrushConverter().ConvertFromString(style.Foreground) as Brush,
                FontSize = style.FontSize,
                FontWeight = FontWeights.Bold
            };
            button.Click += clickHandler;
            return button;
        }
    }
}
