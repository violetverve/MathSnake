using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
using System.Linq;


public class MathUnit: MonoBehaviour
{
    public TextMeshProUGUI exampleText;
    public FoodSpawn foodSpawn;

    private int _x;
    private int _y;
    private int _answer;
    
    private static Dictionary<OperationType, Operation> _operations = new Dictionary<OperationType, Operation>();

    private static bool _isInitialized = false;


    public void GenerateValues()
    {
        _x = UnityEngine.Random.Range(0, 10);
        _y = UnityEngine.Random.Range(1, 10);
    }

    private void Start()
    {
        Initialize();
        SetProblem();
    }

    public bool CheckAnswer(int answer)
    {
        return answer == _answer;
    }

    public void SetProblem()
    {
        GenerateValues();
        ExecuteRandomOperation();
        foodSpawn.SetFoodValues(_answer);
    }

    private void ExecuteRandomOperation()
    {
        int randomOperation = UnityEngine.Random.Range(0, _operations.Count);
        OperationType operationType = _operations.Keys.ElementAt(randomOperation);
        _answer = ApplyOperation(operationType, _x, _y);
        SetExampleText(operationType);
    }

    private static void Initialize()
    {
        List<OperationType> selectedOperations = new List<OperationType>();

        // Load the settings from PlayerPrefs
        if (PlayerPrefs.GetInt("Addition", 0) == 1)
            selectedOperations.Add(OperationType.Addition);

        if (PlayerPrefs.GetInt("Subtraction", 0) == 1)
            selectedOperations.Add(OperationType.Subtraction);

        if (PlayerPrefs.GetInt("Multiplication", 0) == 1)
            selectedOperations.Add(OperationType.Multiplication);

        if (PlayerPrefs.GetInt("Division", 0) == 1)
            selectedOperations.Add(OperationType.Division);

        // If no operation is selected, default to Addition
        if (selectedOperations.Count == 0)
            selectedOperations.Add(OperationType.Addition);

        // Clear the dictionary and initialize the selected operations
        _operations.Clear();

        var assembly = Assembly.GetAssembly(typeof(Operation));
        var allOperationTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Operation)) && !t.IsAbstract);

        foreach (var operationType in allOperationTypes)
        {
            Operation operation = (Operation)Activator.CreateInstance(operationType);
            if (selectedOperations.Contains(operation.OperationType))
            {
                _operations.Add(operation.OperationType, operation);
            }
        }

        _isInitialized = true;
    }

    public static int ApplyOperation(OperationType operationType, int a, int b)
    {
        if (!_isInitialized)
        {
            Initialize();
        }

        if (_operations.ContainsKey(operationType))
        {
            return _operations[operationType].PerformOperation(a, b);
        }

        return 0;
    }

    private void SetExampleText(OperationType operationType)
    {
        if (_operations.ContainsKey(operationType))
        {
            exampleText.text =  _operations[operationType].GetExampleText(_x, _y);
        }
    }


}
