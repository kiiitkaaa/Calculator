using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    class CalculatorClass
    {
        public string Expression { get; set; }
        private string Answer { get; set; }

        public string GetAnswer()
        {
            try
            {
                Answer = new DataTable().Compute(Expression, null).ToString();
                return Answer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(caption: "Ошибка", messageBoxText: "Неправильный ввод!");
                return "долбоёб";
            }
        }
    }
}
