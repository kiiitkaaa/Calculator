using Calculator.Buttons;
using Calculator.Commands;
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
        private readonly CommandInvoker commandInvoker = new(); 

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
            var value = ((Button)sender).Content.ToString();
            var command = new AddCommand(calculator, value);
            commandInvoker.ExecuteCommand(command);
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnOperationClick(object sender, RoutedEventArgs e)
        {
            var value = ((Button)sender).Content.ToString();
            var command = new AddCommand(calculator, value);
            commandInvoker.ExecuteCommand(command);
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            var command = new ClearCommand(calculator);
            commandInvoker.ExecuteCommand(command);
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnEqualClick(object sender, RoutedEventArgs e)
        {
            commandInvoker.ExecuteCommand(new EvaluateCommand(calculator));
            TextBlockAnswer.Text = calculator.GetAnswer();
        }

        private void OnHistoryClick(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new();
            historyWindow.ShowDialog();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var command = new DeleteCommand(calculator);
            commandInvoker.ExecuteCommand(command);
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnFloatClick(object sender, RoutedEventArgs e)
        {
            var command = new FloatPointCommand(calculator);
            commandInvoker.ExecuteCommand(command);
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void OnReverseClick(object sender, RoutedEventArgs e)
        {
            var command = new ReverseSignCommand(calculator);
            commandInvoker.ExecuteCommand(command);
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            commandInvoker.Undo();
            TextBlockAnswer.Text = calculator.Expression;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            calculator.Expression = "";
            TextBlockAnswer.Text = "";
            commandInvoker.Clear();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                commandInvoker.Undo();
                TextBlockAnswer.Text = calculator.Expression;
                return; 
            }
            else if (e.Key == Key.Escape)
            {
                calculator.Expression = "";
                TextBlockAnswer.Text = "";
                commandInvoker.Clear();
                return;
            }

            string? input = null;

            switch (e.Key)
            {
                case Key.D0:
                    input = "0";
                    break;
                case Key.D1:
                    input = "1";
                    break;
                case Key.D2:
                    input = "2";
                    break;
                case Key.D3:
                    input = "3";
                    break;
                case Key.D4:
                    input = "4";
                    break;
                case Key.D5:
                    input = "5";
                    break;
                case Key.D6:
                    input = "6";
                    break;
                case Key.D7:
                    input = "7";
                    break;
                case Key.D8:
                    input = "8";
                    break;
                case Key.D9:
                    input = "9";
                    break;
                case Key.Add:
                    input = "+";
                    break;
                case Key.Subtract:
                    input = "-";
                    break;
                case Key.Multiply:
                    input = "*";
                    break;
                case Key.Divide:
                    input = "/";
                    break;
                case Key.Enter:
                    commandInvoker.ExecuteCommand(new EvaluateCommand(calculator));
                    TextBlockAnswer.Text = calculator.Expression;
                    break;
                case Key.Back:
                    if (!string.IsNullOrEmpty(calculator.Expression))
                    {
                        commandInvoker.ExecuteCommand(new DeleteCommand(calculator));
                        TextBlockAnswer.Text = calculator.Expression;
                    }
                    break;
                case Key.Delete:
                    commandInvoker.ExecuteCommand(new ClearCommand(calculator));
                    TextBlockAnswer.Text = calculator.Expression;
                    break;
                case Key.OemPeriod:
                    var command = new FloatPointCommand(calculator);
                    commandInvoker.ExecuteCommand(command);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(input))
                commandInvoker.ExecuteCommand(new AddCommand(calculator, input));

            TextBlockAnswer.Text = calculator.Expression;
        }
    }
}