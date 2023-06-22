using System.Collections;
using System.Collections.Generic;

public abstract class Operation
{
    public Operation() {}

    public abstract OperationType OperationType { get; }
    public abstract int PerformOperation(int a, int b);
    public abstract char GetOperatorSymbol();
    
    public virtual string GetExampleText(int a, int b)
    {
        return $"{a} {GetOperatorSymbol()} {b} = ?";
    }

    public virtual (int, int) GenerateValues(int min, int max)
    {
        return (UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
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
}

public class DivisionOperation : Operation
{
    public override OperationType OperationType => OperationType.Division;

    public override int PerformOperation(int a, int b)
    {
        return a;
    }

    public override char GetOperatorSymbol()
    {
        return '/';
    }

    public override string GetExampleText(int a, int b)
    {
        return $"{a * b} {GetOperatorSymbol()} {b} = ?";
    }
}