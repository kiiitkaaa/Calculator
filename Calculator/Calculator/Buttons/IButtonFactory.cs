using System.Windows;
using System.Windows.Controls;

namespace Calculator.Buttons
{
    interface IButtonFactory
    {
        Button CreateButton(string content, RoutedEventHandler clickHandler);
    }
}
