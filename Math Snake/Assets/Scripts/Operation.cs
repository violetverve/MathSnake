using System.Collections;
using System.Collections.Generic;

public abstract class Operation
{
    public int _maxValue = 0;
    public Operation() { }

    public int MaxValue => _maxValue;
    public abstract OperationType OperationType { get; }
    public abstract int PerformOperation(int a, int b);
    public abstract char GetOperatorSymbol();
    public abstract void SetMaxValue(int maxValueAddSub, int maxValueMultDiv);

    public virtual string GetExampleText(int a, int b)
    {
        return $"{a} {GetOperatorSymbol()} {b} = ?";
    }

    public virtual (int, int) GenerateValues(int min, int max)
    {
        int maxV = (_maxValue != 0) ? _maxValue : max;
        return (UnityEngine.Random.Range(min, maxV), UnityEngine.Random.Range(min, maxV));
    }

    public virtual List<int> GetPossibleAnswers(int answer, int numberOfFood, int barrier, int x = 0)
    {
        int min = answer - barrier;
        int max = answer + barrier;

        List<int> possibleAnswers = new List<int>();
        possibleAnswers.Add(answer);

        for (int i = 0; i < numberOfFood; i++)
        {
            int possibleAnswer = UnityEngine.Random.Range(min, max);
            while (possibleAnswers.Contains(possibleAnswer))
            {
                possibleAnswer = UnityEngine.Random.Range(min, max);
            }
            possibleAnswers.Add(possibleAnswer);
        }

        return possibleAnswers;
    }
}
