using System.Collections;
using System.Collections.Generic;

public abstract class Operation
{
    public int _maxValue = 0;
    public Operation() {}

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

public class AdditionOperation : Operation
{

    public override OperationType OperationType => OperationType.Addition;
    public override int PerformOperation(int a, int b)
    {
        return a + b;
    }

    public override char GetOperatorSymbol()
    {
        return '+';
    }

    public override void SetMaxValue(int maxValueAddSub, int maxValueMultDiv)
    {
        _maxValue = maxValueAddSub;
    }
}

public class SubtractionOperation : Operation
{
    public override OperationType OperationType => OperationType.Subtraction;

    public override int PerformOperation(int a, int b)
    {
        return a - b;
    }

    public override char GetOperatorSymbol()
    {
        return '-';
    }

    public override void SetMaxValue(int maxValueAddSub, int maxValueMultDiv)
    {
        _maxValue = maxValueAddSub;
    }
}


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

public class DivisionOperation : Operation
{
    public override OperationType OperationType => OperationType.Division;

    public override int PerformOperation(int a, int b)
    {
        return b;
    }

    public override char GetOperatorSymbol()
    {
        return '/';
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