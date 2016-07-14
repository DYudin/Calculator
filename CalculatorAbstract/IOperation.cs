using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
