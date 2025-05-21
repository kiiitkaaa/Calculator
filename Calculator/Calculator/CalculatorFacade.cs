using Calculator.Commands;

namespace Calculator
{
    public class CalculatorFacade
    {
        private readonly CalculatorClass calculator = new();
        private readonly CommandInvoker commandInvoker = new();
        private string? lastAnswer = "";

        public void Evaluate()
        {
            commandInvoker.ExecuteCommand(new EvaluateCommand(calculator));
            lastAnswer = calculator.GetAnswer();
        }

        public string? GetAnswer() => lastAnswer;
        public string GetExpression() => calculator.Expression;
        public void Operation(string? value) => commandInvoker.ExecuteCommand(new AddCommand(calculator, value));
        public void Clear() => commandInvoker.ExecuteCommand(new ClearCommand(calculator));
        public void Delete() => commandInvoker.ExecuteCommand(new DeleteCommand(calculator));
        public void FloatPoint() => commandInvoker.ExecuteCommand(new FloatPointCommand(calculator));
        public void ReverseSign() => commandInvoker.ExecuteCommand(new ReverseSignCommand(calculator));
        public void Undo() => commandInvoker.Undo();
        public void ClearHistory() => commandInvoker.Clear();
    }
}
