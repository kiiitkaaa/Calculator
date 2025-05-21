namespace Calculator.Commands
{
    public class AddCommand(CalculatorClass calculator, string? value) : ICalculatorCommand
    {
        private readonly CalculatorClass calculator = calculator;
        private readonly string? value = value;
        private int lengthBefore;

        public void Execute()
        {
            lengthBefore = calculator.Expression.Length;
            calculator.Expression += value;
        }

        public void Undo()
        {
            calculator.Expression = calculator.Expression[..lengthBefore];
        }
    }
}
