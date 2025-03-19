using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для HistoryUserControl.xaml
    /// </summary>
    public partial class HistoryUserControl : UserControl
    {
        public HistoryUserControl()
        {
            InitializeComponent();
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
