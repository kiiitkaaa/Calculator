using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Calculator.Buttons
{
    interface IButtonFactory
    {
        Button CreateButton(string content, RoutedEventHandler clickHandler);
    }
}
