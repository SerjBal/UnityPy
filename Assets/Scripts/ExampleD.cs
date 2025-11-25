using TMPro;
using UnityEngine;

public class ExamplesD : ExampleBase
{
    [SerializeField] private TMP_InputField _inputField;


    /// <summary>
    /// Execute Python code and return dynamic scope for accessing multiple variables
    /// </summary>
    public override void Run()
    {
        var scriptRunner = DI.Get<IScriptRunner>();

        dynamic scope = scriptRunner.ExecuteCodeWithScope(_inputField.text);

        string name = scope.name;
        float version = scope.version;
        int[] numbers = scope.numbers.As<int[]>();


        PrintResult($"{name}\n{version}:\n{string.Join(", ", numbers)}");
    }
}