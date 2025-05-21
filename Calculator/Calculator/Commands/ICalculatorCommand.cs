namespace Calculator.Commands
{
    public interface ICalculatorCommand
    {
        void Execute();
        void Undo();
    }
}
