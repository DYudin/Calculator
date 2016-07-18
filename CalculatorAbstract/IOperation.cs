
namespace Calculator.Abstract
{
    public interface IOperation
    {
        double ExecuteOperation(params double[] parameters);

        int Priority { get; }

        string Sign { get; }

        int NumberOfParameters { get; }
    }
}
