namespace Calculator.Commands
{
    class EvaluateCommand(CalculatorClass calculator) : ICalculatorCommand
    {
        private readonly CalculatorClass calculator = calculator;
        private readonly string previousExpression = calculator.Expression;

        public void Execute()
        {
            calculator.Evaluate();
        }

        public void Undo()
        {
            calculator.Expression = previousExpression;
        }
    }
}
