using Calculator.Config;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для HistoryUserControl.xaml
    /// </summary>
    public partial class HistoryUserControl : UserControl
    {
        private readonly ElementStyle style;
        public HistoryUserControl(ElementStyle style)
        {
            InitializeComponent();

            this.style = style;
            RootGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.Background));
            ExpressionBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.Foreground));
            AnswerBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(style.Foreground));
        }

        public String? Expression
        {
            get { return GetValue(ExpressionProperty).ToString(); }
            set { SetValue(ExpressionProperty, value); }
        }

        public static readonly DependencyProperty ExpressionProperty =
            DependencyProperty.Register("Expression", typeof(String), typeof(HistoryUserControl));

        public String? Answer
        {
            get { return GetValue(AnswerProperty).ToString(); }
            set { SetValue(AnswerProperty, value); }
        }

        public static readonly DependencyProperty AnswerProperty =
            DependencyProperty.Register("Answer", typeof(String), typeof(HistoryUserControl));
    }
}
