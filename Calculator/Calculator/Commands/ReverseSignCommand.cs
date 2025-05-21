namespace Calculator.Commands
{
    public class ReverseSignCommand(CalculatorClass calculator) : ICalculatorCommand
    {
        private readonly CalculatorClass calculator = calculator;

        public void Execute()
        {
            calculator.Expression += "*(-1)";
        }

        public void Undo()
        {
            if (calculator.Expression.EndsWith("*(-1)"))
            {
                calculator.Expression = calculator.Expression[..^6];
            }
        }
    }
}
