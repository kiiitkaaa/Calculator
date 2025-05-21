namespace Calculator.Commands
{
    class FloatPointCommand(CalculatorClass calculator) : ICalculatorCommand
    {
        private readonly CalculatorClass calculator = calculator;

        public void Execute()
        {
            calculator.Expression += ".";
        }

        public void Undo()
        {
            if (calculator.Expression.EndsWith("."))
            {
                calculator.Expression = calculator.Expression[..^1];
            }
        }
    }
}
