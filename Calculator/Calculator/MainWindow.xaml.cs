using Calculator.Buttons;
using Calculator.Config;
using System.IO;
using System.Media;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Resources;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CalculatorFacade calculatorFacade;
        private ThemeConfig? currentTheme;

        public MainWindow()
        {
            InitializeComponent();

            ApplyTheme("Config/ClassicTheme.json");
            calculatorFacade = new CalculatorFacade();

            Uri uri = new("pack://application:,,,/Resources/pointer.cur");
            StreamResourceInfo sri = Application.GetResourceStream(uri);
            this.Cursor = new Cursor(sri.Stream);
        }

        private void ApplyTheme(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                currentTheme = JsonSerializer.Deserialize<ThemeConfig>(json);

                ButtonsGrid.Children.Clear();

                CreateButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке темы: {ex.Message}");
            }
        }

        public void CreateButtons()
        {
            var digitFactory = new DigitButtonFactory(currentTheme?.DigitButton);
            var operationFactory = new OperationButtonFactory(currentTheme?.OperationButton);
            var functionFactory = new FunctionButtonFactory(currentTheme?.FunctionButton);

            string[] digits = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0"];
            string[] operations = ["+", "-", "*", "/", "="];
            string[] functions = ["AC", "History", "+/-", "<-", ","];

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
            button.Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/pointerBlack.cur")).Stream);
        }

        private void OnDigitClick(object sender, RoutedEventArgs e)
        {
            var value = ((Button)sender).Content.ToString();
            calculatorFacade.Operation(value);
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void OnOperationClick(object sender, RoutedEventArgs e)
        {
            var value = ((Button)sender).Content.ToString();
            calculatorFacade.Operation(value);
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            calculatorFacade.Clear();
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void OnEqualClick(object sender, RoutedEventArgs e)
        {
            calculatorFacade.Evaluate();
            TextBlockAnswer.Text = calculatorFacade.GetAnswer();
        }

        private void OnHistoryClick(object sender, RoutedEventArgs e)
        {
            var stream = new MemoryStream(Properties.Resources.SoundNotification);
            SoundPlayer player = new(stream);
            player.Play();
            HistoryWindow historyWindow = new(currentTheme);
            historyWindow.ShowDialog();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            calculatorFacade.Delete();
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void OnFloatClick(object sender, RoutedEventArgs e)
        {
            calculatorFacade.FloatPoint();
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void OnReverseClick(object sender, RoutedEventArgs e)
        {
            calculatorFacade.ReverseSign();
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            calculatorFacade.Undo();
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            calculatorFacade.Clear();
            calculatorFacade.ClearHistory();
            TextBlockAnswer.Text = calculatorFacade.GetExpression();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                calculatorFacade.Undo();
                TextBlockAnswer.Text = calculatorFacade.GetExpression();
                return;
            }
            else if (e.Key == Key.Escape)
            {
                calculatorFacade.Clear();
                calculatorFacade.ClearHistory();
                TextBlockAnswer.Text = calculatorFacade.GetExpression();
                return;
            }

            string? input = null;

            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    input = "0";
                    break;
                case Key.D1:
                case Key.NumPad1:
                    input = "1";
                    break;
                case Key.D2:
                case Key.NumPad2:
                    input = "2";
                    break;
                case Key.D3:
                case Key.NumPad3:
                    input = "3";
                    break;
                case Key.D4:
                case Key.NumPad4:
                    input = "4";
                    break;
                case Key.D5:
                case Key.NumPad5:
                    input = "5";
                    break;
                case Key.D6:
                case Key.NumPad6:
                    input = "6";
                    break;
                case Key.D7:
                case Key.NumPad7:
                    input = "7";
                    break;
                case Key.D8:
                case Key.NumPad8:
                    input = "8";
                    break;
                case Key.D9:
                case Key.NumPad9:
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
                    calculatorFacade.Evaluate();
                    TextBlockAnswer.Text = calculatorFacade.GetAnswer();
                    return;
                case Key.Back:
                    calculatorFacade.Delete();
                    TextBlockAnswer.Text = calculatorFacade.GetExpression();
                    return;
                case Key.Delete:
                    calculatorFacade.Clear();
                    TextBlockAnswer.Text = calculatorFacade.GetExpression();
                    return;
                case Key.OemPeriod:
                case Key.Decimal:
                    calculatorFacade.FloatPoint();
                    TextBlockAnswer.Text = calculatorFacade.GetExpression();
                    return;
            }

            if (!string.IsNullOrEmpty(input))
            {
                calculatorFacade.Operation(input);
                TextBlockAnswer.Text = calculatorFacade.GetExpression();
            }
        }

        private void ClassicStyleButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Config/ClassicTheme.json");
        }

        private void DarkStyleButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Config/DarkTheme.json");
        }

        private void AnimationBtton_Click(object sender, RoutedEventArgs e)
        {
            SplashVideo.Position = TimeSpan.Zero;
            SplashVideo.Play();
            SplashScreen.Visibility = Visibility.Visible;
        }

        private void SplashScreen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SplashVideo.Stop();
            SplashScreen.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SplashVideo.Source = new Uri("Resources/SplashAnimation.wmv", UriKind.Relative);
            SplashScreen.Visibility = Visibility.Collapsed;
        }
    }
}