namespace Calculator.Commands
{
    class ClearCommand(CalculatorClass calculator) : ICalculatorCommand
    {
        private readonly CalculatorClass calculator = calculator;
        private string oldExpression = "";

        public void Execute()
        {
            oldExpression = calculator.Expression;
            calculator.Expression = "";
        }

        public void Undo()
        {
            calculator.Expression = oldExpression;
        }
    }
}
