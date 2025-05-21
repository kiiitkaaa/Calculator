using Calculator.Config;
using System.Windows;
using System.Windows.Media;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private readonly ThemeConfig? currentTheme;

        public HistoryWindow(ThemeConfig? currentTheme)
        {
            InitializeComponent();
            this.currentTheme = currentTheme;
            ApplyTheme();
            AddContainer();
        }

        private void ApplyTheme()
        {
            var style = currentTheme?.HistoryWindow;
            ButtonClear.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.Background));
            ButtonClear.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.Foreground));
            ButtonClear.FontSize = style.FontSize;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            CalculatorClass.ClearHistory();
        }

        private void AddContainer()
        {
            var history = CalculatorClass.LoadHistory();
            foreach (var entry in history)
            {
                var newControl = new HistoryUserControl(currentTheme?.HistoryControl)
                {
                    Expression = entry["Expression"],
                    Answer = entry["Answer"]
                };

                PastCalculations.Children.Add(newControl);
            }
        }
    }
}
