using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    CalculatorClass calculatorClass = new CalculatorClass();

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
                break;
            case Key.Subtract: // -
                break;
            case Key.Multiply: // *
                break;
            case Key.Divide: // /
                break;
            case Key.Enter: // =
                TextBlockAnswer.Text += "=" + calculatorClass.GetAnswer();
                break;
            case Key.Back: // Backspace
                calculatorClass.Expression.Remove(calculatorClass.Expression.Length - 1);
                break;
            case Key.Delete:
                break;
            case Key.OemPeriod: // .
                break;
            default:
                break;
        }
        TextBlockAnswer.Text = calculatorClass.Expression;
    }
}