using UnityEngine;
using TMPro;

public class ExamplesC : ExampleBase
{
    [SerializeField] private TMP_InputField _inputField;


    /// <summary>
    /// Execute Python code from string and return result
    /// </summary>
    public override void Run()
    {
        var scriptRunner = DI.Get<IScriptRunner>();

        float result = scriptRunner.ExecuteCode<float>(_inputField.text, "result");

        PrintResult(result.ToString());
    }
}