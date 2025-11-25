using TMPro;
using UnityEngine;

public class ExamplesB : ExampleBase
{
    [SerializeField] private TMP_InputField _inputField;


    /// <summary>
    /// Call specific methods of class
    /// </summary>
    public override void Run()
    {
        var scriptRunner = DI.Get<IScriptRunner>();
        float inputNumber = GetInputNumber();

        dynamic testScript = scriptRunner.GetModule("ExampleB"); // Get script module in TestB.py
        dynamic CalculatorClass = testScript.SqrtCalculator(inputNumber); // Get module's class with some initial value 
        float sqrt = CalculatorClass.calc_sqrt_numpy();  // Using class's method with return value

        PrintResult($"sqrt({inputNumber}) = {sqrt}");
    }

    private float GetInputNumber()
    {
        if (float.TryParse(_inputField.text, out var value))
            return value;
        return 0.0f;
    }
}