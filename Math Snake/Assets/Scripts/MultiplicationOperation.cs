using System.Collections;
using System.Collections.Generic;

public class MultiplicationOperation : Operation
{
    public override OperationType OperationType => OperationType.Multiplication;

    public override int PerformOperation(int a, int b)
    {
        return a * b;
    }

    public override char GetOperatorSymbol()
    {
        return '*';
    }

    public override void SetMaxValue(int maxValueAddSub, int maxValueMultDiv)
    {
        _maxValue = maxValueMultDiv;
    }

    public override List<int> GetPossibleAnswers(int answer, int numberOfFood, int barrier, int x)
    {
        int y = answer / x;

        List<int> possibleAnswers = new List<int>();
        possibleAnswers.Add(answer);

        int sign = 1;

        for (int i = 1; i < numberOfFood; i++)
        {
            int possibleAnswer = x * (y + sign * i);
            possibleAnswers.Add(possibleAnswer);
            sign *= -1;
        }

        return possibleAnswers;
    }

    public override (int, int) GenerateValues(int min, int max)
    {
        int maxV = (_maxValue != 0) ? _maxValue : max;
        return (UnityEngine.Random.Range(min, maxV), UnityEngine.Random.Range(1, 10));
    }
}
