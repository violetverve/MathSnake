using System.Collections;
using System.Collections.Generic;

public class DivisionOperation : Operation
{
    public override OperationType OperationType => OperationType.Division;

    public override int PerformOperation(int a, int b)
    {
        return b;
    }

    public override string GetOperatorSymbol()
    {
        return "<sprite name=\"DivisionSign\">";
    }

    public override string GetExampleText(int a, int b)
    {
        return $"{a * b} {GetOperatorSymbol()} {a} = ?";
    }

    public override void SetMaxValue(int maxValueAddSub, int maxValueMultDiv)
    {
        _maxValue = maxValueMultDiv;
    }

    public override (int, int) GenerateValues(int min, int max)
    {
        int maxV = (_maxValue != 0) ? _maxValue : max;
        return (UnityEngine.Random.Range(min, maxV), UnityEngine.Random.Range(1, 10));
    }
}
