namespace Calculator.Commands
{
    class DeleteCommand(CalculatorClass calculator) : ICalculatorCommand
    {
        private readonly CalculatorClass calculator = calculator;
        private string removedChar = "";

        public void Execute()
        {
            if (!string.IsNullOrEmpty(calculator.Expression))
            {
                removedChar = calculator.Expression[^1].ToString();
                calculator.Expression = calculator.Expression[..^1];
            }
        }

        public void Undo()
        {
            calculator.Expression += removedChar;
        }
    }
}
