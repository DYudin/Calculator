
namespace Calculator.Abstract
{
    public interface IOperation
    {
        float ExecuteOperation(params float[] parameters);

        int Priority { get; }
        
        string Sign { get; }

        int NumberOfParameters { get; }
    }
}
