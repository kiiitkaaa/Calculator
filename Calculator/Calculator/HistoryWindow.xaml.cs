using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            AddContainer();
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
                var newControl = new HistoryUserControl
                {
                    Expression = entry["Expression"],
                    Answer = entry["Answer"]
                };

                PastCalculations.Children.Add(newControl);
            }
        }
    }
}
