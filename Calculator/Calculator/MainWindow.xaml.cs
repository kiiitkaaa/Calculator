using Calculator.Buttons;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CalculatorClass calculator = new();

        public MainWindow()
        {
            InitializeComponent();

            CreateButtons();
        }

        public void CreateButtons()
        {
            var digitFactory = new DigitButtonFactory();
            var operationFactory = new OperationButtonFactory();
            var functionFactory = new FunctionButtonFactory();

            string[] digits = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string[] operations = { "+", "-", "*", "/", "=" };
            string[] functions = { "AC", "History", "+/-", "<-" , "," };

            int row = 0, col = 0;

            foreach (var digit in digits)
            {
                for (int i = 1; i <= 9; i++)
                {
                    int rowD = (i - 1) / 3;
                    int colD = (i - 1) % 3 + 1;
                    if (digit == "0") 
                    {
                        AddButton(digitFactory.CreateButton("0", OnDigitClick), 3, 2);
                        
                    }
                    else
                        AddButton(digitFactory.CreateButton(i.ToString(), OnDigitClick), rowD, colD);
                }
            }

            foreach (var operation in operations)
            {
                if (operation == "/") { row = 0; col = 4; }
                if (operation == "*") { row = 1; col = 4; }
                if (operation == "-") { row = 2; col = 4; }
                if (operation == "+") { row = 3; col = 4; }
                if (operation == "=") { row = 3; col = 3; }

                RoutedEventHandler handler;

                if (operation == "=")
                {
                    handler = OnEqualClick;
                }
                else
                {
                    handler = OnOperationClick;
                }

                AddButton(operationFactory.CreateButton(operation, handler), row, col);
            }

            foreach (var function in functions)
            {
                if (function == "AC") { row = 0; col = 0; }
                if (function == "<-") { row = 1; col = 0; }
                if (function == "+/-") { row = 2; col = 0; }
                if (function == "History") { row = 3; col = 0; }
                if (function == ",") { row = 3; col = 1; }

                RoutedEventHandler handler = function switch
                {
                    "AC" => OnClearClick,
                    "=" => OnEqualClick,
                    "History" => OnHistoryClick,
                    "<-" => OnDeleteClick,
                    "+/-" => OnReverseClick,
                    "," => OnFloatClick,
                    _ => throw new Exception("Error")
                };
                AddButton(functionFactory.CreateButton(function, handler), row, col);
            }
        }

        private void AddButton(Button button, int row, int col)
        {
            Grid.SetRow(button, row);
            Grid.SetColumn(button, col);
            ButtonsGrid.Children.Add(button);
        }

        private void OnDigitClick(object sender, RoutedEventArgs e)
        {
            calculator.Expression += ((Button)sender).Content.ToString();
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnOperationClick(object sender, RoutedEventArgs e)
        {
            calculator.Expression += ((Button)sender).Content.ToString();
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            calculator.Expression = "";
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnEqualClick(object sender, RoutedEventArgs e)
        {
            TextBlockAnswer.Text = calculator.GetAnswer();
        }

        private void OnHistoryClick(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(calculator.Expression))
            {
                calculator.Expression = calculator.Expression.Remove(calculator.Expression.Length - 1);
                TextBlockAnswer.Text = calculator.Expression;
            }
        }

        private void OnFloatClick(object sender, RoutedEventArgs e)
        {
            calculator.Expression += ".";
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnReverseClick(object sender, RoutedEventArgs e)
        {
            calculator.Expression += "*(-1)";
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                    calculator.Expression += "0";
                    break;
                case Key.D1:
                    calculator.Expression += "1";
                    break;
                case Key.D2:
                    calculator.Expression += "2";
                    break;
                case Key.D3:
                    calculator.Expression += "3";
                    break;
                case Key.D4:
                    calculator.Expression += "4";
                    break;
                case Key.D5:
                    calculator.Expression += "5";
                    break;
                case Key.D6:
                    calculator.Expression += "6";
                    break;
                case Key.D7:
                    calculator.Expression += "7";
                    break;
                case Key.D8:
                    calculator.Expression += "8";
                    break;
                case Key.D9:
                    calculator.Expression += "9";
                    break;
                case Key.Add:
                    calculator.Expression += "+";
                    break;
                case Key.Subtract:
                    calculator.Expression += "-";
                    break;
                case Key.Multiply:
                    calculator.Expression += "*";
                    break;
                case Key.Divide:
                    calculator.Expression += "/";
                    break;
                case Key.Enter:
                    TextBlockAnswer.Text = calculator.GetAnswer();
                    break;
                case Key.Back:
                    if (!string.IsNullOrEmpty(calculator.Expression))
                    {
                        calculator.Expression = calculator.Expression.Remove(calculator.Expression.Length - 1);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case Key.Delete:
                    calculator.Expression = "";
                    break;
                case Key.OemPeriod:
                    calculator.Expression += ".";
                    break;
                default:
                    break;
            }
            TextBlockAnswer.Text = calculator.Expression;
        }
    }
}