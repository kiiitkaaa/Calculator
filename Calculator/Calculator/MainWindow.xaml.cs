using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CalculatorClass calculatorClass = new CalculatorClass();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                    calculatorClass.Expression += "0";
                    break;
                case Key.D1:
                    calculatorClass.Expression += "1";
                    break;
                case Key.D2:
                    calculatorClass.Expression += "2";
                    break;
                case Key.D3:
                    calculatorClass.Expression += "3";
                    break;
                case Key.D4:
                    calculatorClass.Expression += "4";
                    break;
                case Key.D5:
                    calculatorClass.Expression += "5";
                    break;
                case Key.D6:
                    calculatorClass.Expression += "6";
                    break;
                case Key.D7:
                    calculatorClass.Expression += "7";
                    break;
                case Key.D8:
                    calculatorClass.Expression += "8";
                    break;
                case Key.D9:
                    calculatorClass.Expression += "9";
                    break;
                case Key.Add: // +
                    calculatorClass.Expression += "+";
                    break;
                case Key.Subtract: // -
                    calculatorClass.Expression += "-";
                    break;
                case Key.Multiply: // *
                    calculatorClass.Expression += "*";
                    break;
                case Key.Divide: // /
                    calculatorClass.Expression += "/";
                    break;
                case Key.Enter: // =
                    TextBlockAnswer.Text = calculatorClass.GetAnswer();
                    break;
                case Key.Back: // Backspace
                    if (!string.IsNullOrEmpty(calculatorClass.Expression))
                    {
                        calculatorClass.Expression = calculatorClass.Expression.Remove(calculatorClass.Expression.Length - 1);
                        break;
                    }
                    else
                    {
                        break;
                    }
                case Key.Delete:
                    calculatorClass.Expression = "";
                    break;
                case Key.OemPeriod: // .
                    calculatorClass.Expression += ".";
                    break;
                default:
                    break;
            }
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "1";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "2";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "3";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "4";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "5";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "6";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "7";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "8";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "9";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "0";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }

        private void ButtonFloat_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += ".";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonAC_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression = "";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonDeleteLast_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(calculatorClass.Expression))
            {
                calculatorClass.Expression = calculatorClass.Expression.Remove(calculatorClass.Expression.Length - 1);
                TextBlockAnswer.Text = calculatorClass.Expression;
            }
        }

        private void ButtonReverse_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "*(-1)";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            TextBlockAnswer.Text = calculatorClass.GetAnswer();
        }

        private void ButtonDvision_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "/";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonMultiplication_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "*";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "-";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            calculatorClass.Expression += "+";
            TextBlockAnswer.Text = calculatorClass.Expression;
        }
    }
}