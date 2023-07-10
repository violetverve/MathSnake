using System.Collections;
using System.Collections.Generic;

public class AdditionOperation : Operation
{

    public override OperationType OperationType => OperationType.Addition;
    public override int PerformOperation(int a, int b)
    {
        return a + b;
    }

    public override string GetOperatorSymbol()
    {
        return "+";
    }

    public override void SetMaxValue(int maxValueAddSub, int maxValueMultDiv)
    {
        _maxValue = maxValueAddSub;
    }
}
