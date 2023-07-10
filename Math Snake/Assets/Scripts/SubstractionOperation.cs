using System.Collections;
using System.Collections.Generic;
public class SubtractionOperation : Operation
{
    public override OperationType OperationType => OperationType.Subtraction;

    public override int PerformOperation(int a, int b)
    {
        return a - b;
    }

    public override string GetOperatorSymbol()
    {
        return "-";
    }

    public override void SetMaxValue(int maxValueAddSub, int maxValueMultDiv)
    {
        _maxValue = maxValueAddSub;
    }
}
