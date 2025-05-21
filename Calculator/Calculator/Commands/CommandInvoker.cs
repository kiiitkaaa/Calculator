namespace Calculator.Commands
{
    public class CommandInvoker
    {
        private readonly Stack<ICalculatorCommand> commandHistory = new();

        public void ExecuteCommand(ICalculatorCommand command)
        {
            command.Execute();
            commandHistory.Push(command);
        }

        public void Undo()
        {
            if (commandHistory.Count > 0)
            {
                var command = commandHistory.Pop();
                command.Undo();
            }
        }

        public void Clear()
        {
            commandHistory.Clear();
        }
    }
}
