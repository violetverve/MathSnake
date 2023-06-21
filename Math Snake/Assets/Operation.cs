public abstract class Operation
{
    public Operation() {}

    public abstract OperationType OperationType { get; }
    public abstract int PerformOperation(int a, int b);
    public abstract char GetOperatorSymbol();
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
}

public class DivisionOperation : Operation
{
    public override OperationType OperationType => OperationType.Division;

    public override int PerformOperation(int a, int b)
    {
        return a / b;
    }

    public override char GetOperatorSymbol()
    {
        return '/';
    }
}