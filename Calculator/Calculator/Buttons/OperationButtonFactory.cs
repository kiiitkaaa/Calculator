using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Calculator.Buttons
{
    class OperationButtonFactory : IButtonFactory
    {
        public Button CreateButton(string content, RoutedEventHandler clickHandler)
        {
            var button = new Button
            {
                Content = content,
                Background = Brushes.Coral,
                FontSize = 30,
                FontWeight = FontWeights.Bold
            };
            button.Click += clickHandler;
            return button;
        }
    }
}
