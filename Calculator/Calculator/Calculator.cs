using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class CalculatorClass
    {
        public string Expression { get; set; }

        public string GetAnswer()
        {

            string primer = "1+2";
            string first = new DataTable().Compute(primer, null).ToString();
            return "answer";
        }
    }
}
